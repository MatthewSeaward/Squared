using Assets.Scripts.Workers.Piece_Effects.Destruction;
using Assets.Scripts.Workers.Piece_Effects.SwapEffects;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public delegate void BoardRefreshed();

    class PieceDropper : MonoBehaviour
    {
        public static BoardRefreshed BoardRefreshed;
        public static PieceDropper Instance;
        private static int spawnedInRow = 0;
        private static bool checkInProgress = false;
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
                StartCoroutine(CheckForEmptySlots_Async());
            }
            else
            {
                QueuedChecks++;
            }
        }

        private IEnumerator CheckForEmptySlots_Async()
        {
            checkInProgress = true;
            for (int column = 0; column < PieceController.NumberOfColumns; column++)
            {
                int row = 0;
                spawnedInRow = 0;
                while (PieceController.HasEmptySlotInColumn(column, ref row))
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

                    float worldPosY = PieceController.YPositions[row];
                    float worldPosX = PieceController.XPositions[column];

                    var piece = GridGenerator.GenerateTile(worldPosX, 5, column, row);
                    SquarePiece squarePiece = piece.GetComponent<SquarePiece>();

                    if (!PieceController.Pieces.Contains(squarePiece))
                    {
                        PieceController.Pieces.Add(squarePiece);
                    }
                    var fallC = squarePiece.DestroyPieceHandler as DestroyTriggerFall;

                    Lerp lerp = squarePiece.GetComponent<Lerp>();
                    lerp.Setup(new Vector3(worldPosX, worldPosY));

                    squarePiece.Position = new Vector2Int(column, row);

                    yield return new WaitForSeconds(0.1f);
                }
            }

            BoardRefreshed?.Invoke();
            checkInProgress = false;

            if (QueuedChecks > 0)
            {
                QueuedChecks--;
                StartCoroutine(CheckForEmptySlots_Async());
            }
        }
    }
}
