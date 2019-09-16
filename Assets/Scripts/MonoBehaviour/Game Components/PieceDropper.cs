using Assets.Scripts.Workers.Helpers.Extensions;
using Assets.Scripts.Workers.Piece_Effects.SwapEffects;
using System;
using System.Collections;
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

        public void CheckForEmptySlots()
        {
            if (!checkInProgress)
            {
                CheckForEmptySlots_Async();
            }
            else
            {
                QueuedChecks++;
            }
        }

        private void CheckForEmptySlots_Async()
        {
            DebugLogger.Instance.WriteEntry("CheckForEmptySlots - Start");

            checkInProgress = true;
            columnsToCheck = PieceController.NumberOfColumns;

            for (int column = 0; column < PieceController.NumberOfColumns; column++)
            {
                StartCoroutine(CheckForEmptySlotsInColumn(column));
            }

            DebugLogger.Instance.WriteEntry("CheckForEmptySlots - End");

        }

        private IEnumerator CheckForEmptySlotsInColumn(int column)
        {
            DebugLogger.Instance.WriteEntry($"CheckForEmptySlotsInColumn - Start - Column {column}");
            
            int row = 0;

            while (PieceController.HasEmptySlotInColumn(column, ref row))
            {
                try
                {
                    row = GetFirstEmptySlotInRow(column, row);

                    float worldPosY = PieceController.YPositions[row];
                    float worldPosX = PieceController.XPositions[column];

                    DebugLogger.Instance.WriteEntry($"CheckForEmptySlotsInColumn - Spawning piece in {row}, {column}. Wolrd Pos {worldPosX}, {worldPosY}");


                    var piece = GridGenerator.GenerateTile(worldPosX, 5, column, row);
                    SquarePiece squarePiece = piece.GetComponent<SquarePiece>();

                    if (!PieceController.Pieces.Contains(squarePiece))
                    {
                        PieceController.Pieces.Add(squarePiece);
                    }

                    DebugLogger.Instance.WriteEntry($"CheckForEmptySlotsInColumn - Piece Spawned - {squarePiece.Type}");

                    Lerp lerp = squarePiece.GetComponent<Lerp>();
                    lerp.Setup(new Vector3(worldPosX, worldPosY));

                    squarePiece.Position = new Vector2Int(column, row);

                }
                catch (Exception ex)
                {
                    DebugLogger.Instance.WriteException(ex);
                }

                yield return new WaitForSeconds(0.08f);
            }

            ColumnCheckCompleted(column);

            DebugLogger.Instance.WriteEntry($"CheckForEmptySlotsInColumn - End - Column {column}");
        }

        private static int GetFirstEmptySlotInRow(int column, int row)
        {
            for (int i = row; i < PieceController.NumberOfRows; i++)
            {
                var currentPiece = PieceController.GetPiece(column, row);
                if (currentPiece != null && currentPiece.SwapEffect is LockedSwap)
                {
                    row++;
                }
                else if (PieceController.IsEmptySlot(column, row + 1))
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
