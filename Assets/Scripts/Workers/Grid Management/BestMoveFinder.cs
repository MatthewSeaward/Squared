using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Workers.Grid_Management
{
    public class BestMoveFinder
    { 
        public static List<ISquarePiece> GetBestMove()
        {
            var bestMove = new Dictionary<int, List<ISquarePiece>>();
            int currentIndex = 0;
            for (int x = 0; x < PieceController.NumberOfColumns; x++)
            {
                for (int y = 0; y < PieceController.NumberOfRows; y++)
                {
                    var explored = new List<ISquarePiece>();
                    var current = new List<ISquarePiece>();

                    var piece = PieceController.GetPiece(x, y);
                    if (piece == null)
                    {
                        continue;
                    }

                    while(bestMove.ContainsKey(currentIndex))
                    {
                        currentIndex++;
                    }

                    explored.Add(piece);
                    current.Add(piece);
                    bestMove.Add(currentIndex, new List<ISquarePiece>());
                    bestMove[currentIndex].Add(piece);
                    ExplorePiece(ref bestMove, currentIndex, ref current, ref explored, piece);
                }
            }

            var filtered =  bestMove.Values.Where(x => x.Count >= 2);
            var best = filtered.OrderByDescending(x => x.Count).First();

            Debug.Log(string.Join(", ", best.Select(x => x.Position.ToString())));
            return best.ToList();
        }

        private static void ExplorePiece(ref Dictionary<int, List<ISquarePiece>> moves, int currentIndex, ref List<ISquarePiece> current, ref List<ISquarePiece> explored, ISquarePiece piece)
        {
            currentIndex = AddIfValid(ref moves, currentIndex, ref explored, ref current, piece, piece.Position.x + 1, piece.Position.y + 1);
            currentIndex = AddIfValid(ref moves, currentIndex, ref explored, ref current, piece, piece.Position.x + 1, piece.Position.y - 1);
            currentIndex = AddIfValid(ref moves, currentIndex, ref explored, ref current, piece, piece.Position.x + 1, piece.Position.y);
            currentIndex = AddIfValid(ref moves, currentIndex, ref explored, ref current, piece, piece.Position.x, piece.Position.y + 1);
            currentIndex = AddIfValid(ref moves, currentIndex, ref explored, ref current, piece, piece.Position.x, piece.Position.y - 1);
            currentIndex = AddIfValid(ref moves, currentIndex, ref explored, ref current, piece, piece.Position.x  -1, piece.Position.y + 1);
            currentIndex = AddIfValid(ref moves, currentIndex, ref explored, ref current, piece, piece.Position.x - 1, piece.Position.y - 1);
            currentIndex = AddIfValid(ref moves, currentIndex, ref explored, ref current, piece, piece.Position.x - 1, piece.Position.y);
        }

        private static int AddIfValid(ref Dictionary<int, List<ISquarePiece>> moves, int currentIndex, ref List<ISquarePiece> current, ref List<ISquarePiece> explored, ISquarePiece piece, int x, int y)
        {
            if (MoveCheckerHelpers.CheckSpot(piece, x, y))
            {
                if (!moves.ContainsKey(currentIndex))
                {
                    moves.Add(currentIndex, new List<ISquarePiece>());
                }
                var newPiece = PieceController.GetPiece(x, y);
                if (explored.Contains(newPiece))
                {
                    return currentIndex;
                }

                current.Add(newPiece);
                explored.Add(newPiece);
                
                ExplorePiece(ref moves, currentIndex, ref current, ref explored, newPiece);

                currentIndex++;
                if (!moves.ContainsKey(currentIndex))
                {
                    var newList = new List<ISquarePiece>();
                    newList.AddRange(current);
                    moves.Add(currentIndex, newList);
                    current = new List<ISquarePiece>();
                }
                return currentIndex;
            }
            else
            {
                return currentIndex;
            }
        }
    }
}
