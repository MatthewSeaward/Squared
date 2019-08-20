using Assets;
using Assets.Scripts;
using GridGeneration.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace GridGeneration
{
    public delegate void GridGenerated();

    public class BasicGridGenerator : IGridGenerator
    {
        public static event GridGenerated GridGenerated;

        public GameObject GridCell;
       
        public void GenerateTiles(Vector2 Start, float tileSpacing)
        {
            LevelManager.Instance.SelectedLevel = LevelManager.Instance.GetNextLevel();

            var parent = GameObject.FindObjectOfType<PieceSelectionManager>();
            
            var pieces = new List<SquarePiece>();
            
            string[] pattern = LevelManager.Instance.SelectedLevel.Pattern;

            int rows = pattern.Length;
            int columns = pattern[0].Length;

            var YPositions = new float[rows];
            var XPositions = new float[columns];

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    string row = pattern[y];
                    char pieceKey = row[x];

                    if (pieceKey == '-')
                    {
                        continue;
                    }                    

                    float xPos = Start.x + (x * tileSpacing);
                    float yPos = Start.y - (y * tileSpacing);

                    var piece = GenerateTile(pieceKey, xPos, yPos, x, y, true);
                    var cell = ObjectPool.Instantiate(GridCell, new Vector3(xPos, yPos, 0));

                    piece.transform.parent = parent.transform;
                    cell.transform.parent = parent.transform;

                    pieces.Add(piece.GetComponent<SquarePiece>());
                    YPositions[y] = yPos;
                    XPositions[x] = xPos;
                }
            }
            PieceController.Setup(pieces,YPositions, XPositions);

            GridGenerated?.Invoke();
        }

        public GameObject GenerateTile(char pieceKey, float xPos, float yPos, int x, int y, bool initialSetup = false)
        {
            var piece = PieceFactory.Instance.CreateRandomSquarePiece(pieceKey, initialSetup);                
            piece.transform.position = new Vector3(xPos, yPos, 0);

            var square = piece.GetComponent<SquarePiece>();
            square.Position = new Vector2Int(x, y);

            return piece;
        }
    }
}
