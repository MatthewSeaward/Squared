using Assets.Scripts.Workers.Piece_Effects.Destruction;
using Assets.Scripts.Workers.Piece_Effects.SwapEffects;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    class PieceDropper : MonoBehaviour
    {

        public static PieceDropper Instance;

        private GridGenerator _gridGenerator;
    
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
            StartCoroutine(CheckForEmptySlots_Async());
        }

        private IEnumerator CheckForEmptySlots_Async()
        {      
            for (int column = 0; column < PieceController.NumberOfColumns; column++)
            {
                int row = 0;
                while (PieceController.HasEmptySlotInColumn(column, ref row))
                {
                    for (int i = row; i < PieceController.NumberOfRows; i++)
                    {
                        var currentPiece = PieceController.GetPiece(column, row);
                        if (currentPiece != null && currentPiece.SwapEffect is LockedSwap)
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
                    
                    var piece = GridGenerator.GenerateTile(worldPosX, 5,column, column);
                    SquarePiece squarePiece = piece.GetComponent<SquarePiece>();

                    if (!PieceController.Pieces.Contains(squarePiece))
                    {
                        PieceController.Pieces.Add(squarePiece);
                    }
                    var fallC = squarePiece.DestroyPieceHandler as DestroyTriggerFall;                    

                    Lerp lerp = squarePiece.GetComponent<Lerp>();
                    lerp.Setup(new Vector3(worldPosX, worldPosY));

                    squarePiece.Position = new Vector2Int(column, row);

                    yield return new WaitForFixedUpdate();
                }
            }
        }
    }
}
