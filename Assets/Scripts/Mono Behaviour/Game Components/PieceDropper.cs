using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Piece_Effects.Destruction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public delegate void BoardRefreshed();

    class PieceDropper : MonoBehaviour
    {
        public static BoardRefreshed BoardRefreshed;

        public static PieceDropper Instance;

        private static bool checkInProgress = false;
        private int columnsToCheck;
        private GridGenerator _gridGenerator;
        private static int QueuedChecks;
        private readonly int NumberToPrespawn = 1;
        private Dictionary<int, Queue<GameObject>> QueuedPieces = new Dictionary<int, Queue<GameObject>>();
        
        private GridGenerator GridGenerator
        {
            get
            {
                if (_gridGenerator == null)
                {
                    _gridGenerator = GameObject.FindObjectOfType<GridGenerator>();
                }
                return _gridGenerator;
            }
        }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SeedSlots();       
        }

        public void CheckForEmptySlots()
        {
            if (!checkInProgress)
            {
                checkInProgress = true;

                SeedSlots();
                CheckForEmptySlots_Async();
            }
            else
            {
                QueuedChecks++;
            }
        }

        private void SeedSlots()
        {
            for (int column = 0; column < PieceManager.Instance.NumberOfColumns; column++)
            {
                SeedColumn(column);
            }
        }

        private void SeedColumn(int column)
        {
            try
            { 
                if (PieceManager.Instance.HasSlotInColumn(column))
                {
                    return;
                }

                if (!QueuedPieces.ContainsKey(column))
                {
                    QueuedPieces.Add(column, new Queue<GameObject>());
                }

                var pieceQueue = QueuedPieces[column];

                if (pieceQueue.Count >= NumberToPrespawn)
                {
                    return;
                }

                for (int i = 0; i < NumberToPrespawn; i++)
                {
                    float worldPosX = PieceManager.Instance.XPositions[column];

                    var piece = GridGenerator.GenerateRandomTile(worldPosX, 4, column, -1);
                    piece.GetComponent<Collider2D>().enabled = false;

                    pieceQueue.Enqueue(piece);
                }
            }
            catch (Exception ex)
            {
                DebugLogger.Instance.WriteException(ex);
            }
        }

        private GameObject GetPiece(int column)
        {
            var piece = QueuedPieces[column].Dequeue();

            SeedColumn(column);

            return piece;
        }

        private void CheckForEmptySlots_Async()
        {
            try
            {
                columnsToCheck = PieceManager.Instance.NumberOfColumns;

                for (int column = 0; column < PieceManager.Instance.NumberOfColumns; column++)
                {
                    StartCoroutine(CheckForEmptySlotsInColumn(column));
                }
            }
            catch (Exception ex)
            {
                DebugLogger.Instance.WriteException(ex);
            }
        }

        private IEnumerator CheckForEmptySlotsInColumn(int column)
        {
            int row = 0;

            while (PieceManager.Instance.HasEmptySlotInColumn(column, out row))
            {
                try
                {
                    row = GetFirstEmptySlotInRow(column, row);

                    float worldPosY = PieceManager.Instance.YPositions[row];
                    float worldPosX = PieceManager.Instance.XPositions[column];                   

                    var piece = GetPiece(column);

                    piece.GetComponent<Collider2D>().enabled = true;

                    var squarePiece = piece.GetComponent<SquarePiece>();
                    squarePiece.Position = new Vector2Int(column, row);

                    if (!PieceManager.Instance.Pieces.Contains(squarePiece))
                    {
                        PieceManager.Instance.Pieces.Add(squarePiece);
                    }

                    Lerp lerp = squarePiece.GetComponent<Lerp>();
                    lerp.Setup(new Vector3(worldPosX, worldPosY));

                }
                catch (Exception ex)
                {
                    DebugLogger.Instance.WriteException(ex);
                }

                yield return new WaitForSeconds(0.08f);
            }

            ColumnCheckCompleted(column);
        }

        private static int GetFirstEmptySlotInRow(int column, int row)
        {
            for (int i = row; i < PieceManager.Instance.NumberOfRows; i++)
            {
                var currentPiece = PieceManager.Instance.GetPiece(column, row);
                if (currentPiece != null && currentPiece.DestroyPieceHandler is LockedSwap)
                {
                    row++;
                }
                else if (PieceManager.Instance.IsEmptySlot(column, row + 1))
                {
                    row++;
                }
                else
                {
                    break;
                }
            }

            return row;
        }

        private void ColumnCheckCompleted(int column)
        {
            columnsToCheck--;

            if (columnsToCheck == 0)
            {
                BoardRefreshed?.Invoke();
                checkInProgress = false;

                if (QueuedChecks > 0)
                {
                    QueuedChecks--;
                    CheckForEmptySlots_Async();
                }
            }
        }
    }
}
