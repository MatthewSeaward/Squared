using Assets;
using Assets.Scripts.Workers.IO.Data_Entities;
using GridGeneration.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using static PieceFactory;

namespace GridGeneration
{
    public delegate void GridGenerated();

    public class BasicGridGenerator : IGridGenerator
    {
        public static event GridGenerated GridGenerated;

        [SerializeField]
        private Transform GridParent;
       
        public void GenerateTiles(Vector2 Start, float tileSpacing)
        {
            LevelManager.Instance.SelectedLevel = LevelManager.Instance.GetNextLevel();
                       
            string[] pattern = LevelManager.Instance.SelectedLevel.Pattern;

            int rows = pattern.Length;
            int columns = pattern[0].Length;

            var YPositions = new float[rows];
            var XPositions = new float[columns];
            var pieces = new List<ISquarePiece>(rows * columns);

            for (int x = 0; x < columns; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    string row = pattern[y];
                    PieceTypes type = (PieceTypes) row[x];

                    if (type == PieceTypes.Empty)
                    {
                        continue;
                    }                    

                    float xPos = Start.x + (x * tileSpacing);
                    float yPos = Start.y - (y * tileSpacing);

                    var piece = GenerateTile(type, xPos, yPos, x, y, true);
                    var cell = ObjectPool.Instantiate(GameResources.GameObjects["Piece Slot"], new Vector3(xPos, yPos, 0));

                    piece.transform.parent = GridParent;
                    cell.transform.parent = GridParent;

                    pieces.Add(piece.GetComponent<SquarePiece>());
                    YPositions[y] = yPos;
                    XPositions[x] = xPos;
                }
            }
            PieceController.Setup(pieces,XPositions, YPositions);

            GridGenerated?.Invoke();
        }

        public GameObject GenerateTile(PieceTypes type, float xPos, float yPos, int x, int y, bool initialSetup = false)
        {
            var piece = PieceFactory.Instance.CreateRandomSquarePiece(type, initialSetup);                
            piece.transform.position = new Vector3(xPos, yPos);

            var square = piece.GetComponent<SquarePiece>();
            square.Position = new Vector2Int(x, y);

            return piece;
        }
    }
}
