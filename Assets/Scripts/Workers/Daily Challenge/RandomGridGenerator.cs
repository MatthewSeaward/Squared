using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Workers.Factorys;
using UnityEngine;

namespace Assets.Scripts.Workers.Daily_Challenge
{
    public static class RandomGridGenerator
    {
        public static string[] GenerateRandomGrid(INumberGenerator numberGenerator)
        {
            var emptyGrid = GenerateStartingGrid();

            var populatedGrid = GenerateGrid(numberGenerator, emptyGrid);
            populatedGrid = AddSpecialPieces(numberGenerator, populatedGrid);

            return ToStringArray(populatedGrid);
        }

        private static List<GeneratedSpace> AddSpecialPieces(INumberGenerator numberGenerator, List<GeneratedSpace> populatedGrid)
        {
            var numberOfSpecialPieces = numberGenerator.Range(0, 10);

            for (int i = 0; i < numberOfSpecialPieces; i++)
            {
                populatedGrid[numberGenerator.Range(0, populatedGrid.Count)].PieceType = GetRandomSpecialPiece(numberGenerator);
            }

            return populatedGrid;
        }

        private static PieceBuilderDirector.PieceTypes GetRandomSpecialPiece(INumberGenerator numberGenerator)
        {
            var specialPieces = new List<PieceBuilderDirector.PieceTypes>();
            specialPieces.AddRange((PieceBuilderDirector.PieceTypes[])Enum.GetValues(typeof(PieceBuilderDirector.PieceTypes)));
            specialPieces.Remove(PieceBuilderDirector.PieceTypes.Normal);
            specialPieces.Remove(PieceBuilderDirector.PieceTypes.Empty);
            specialPieces.Remove(PieceBuilderDirector.PieceTypes.Heart);
            specialPieces.Remove(PieceBuilderDirector.PieceTypes.Chest);

            return specialPieces[numberGenerator.Range(0, specialPieces.Count)];
        }

        private static List<GeneratedSpace> GenerateGrid(INumberGenerator numberGenerator, List<Vector2Int> emptyGrid)
        {
            var populatedGrid = new List<GeneratedSpace>();

            var lastPiece = GetRandomPosition(numberGenerator, emptyGrid);
            emptyGrid.Remove(lastPiece);
            populatedGrid.Add(new GeneratedSpace() { Position = lastPiece, PieceType = Factorys.PieceBuilderDirector.PieceTypes.Normal });

            var numberOfPositions = numberGenerator.Range(40, 60);

            for (int i = 0; i < numberOfPositions; i++)
            {
                // Order the rest by their distance to that starting piece, and select the closest 3
                var filteredList = emptyGrid.OrderBy(x => Vector2Int.Distance(x, lastPiece)).ThenBy(x => numberGenerator.Next()).Take(3).ToList();
                var nextPiece = GetRandomPosition(numberGenerator, filteredList);

                emptyGrid.Remove(nextPiece);
                populatedGrid.Add(new GeneratedSpace() { Position = nextPiece, PieceType = Factorys.PieceBuilderDirector.PieceTypes.Normal });

                lastPiece = nextPiece;
            }
            return populatedGrid;
        }

        private static List<Vector2Int> GenerateStartingGrid()
        {
            var spaces = new List<Vector2Int>();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    spaces.Add(new Vector2Int(x, y));
                }
            }

            return spaces;
        }

        private static string[] ToStringArray(List<GeneratedSpace> spaces)
        {
            var output = new string[11];

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 11; y++)
                {
                    var space = spaces.FirstOrDefault(s => s.Position.x == x && s.Position.y == y);

                    if (output[y] == null)
                    {
                        output[y] = string.Empty;
                    }

                    output[y] += space == null ? '-' : (char) space.PieceType;
                }
            }

            return output;
        }

        private static Vector2Int GetRandomPosition(INumberGenerator numberGenerator, List<Vector2Int> list)
        {
            return list[numberGenerator.Range(0, list.Count)];
        }
    }
}
