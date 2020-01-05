using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Workers.Grid_Management
{
    public class BestMoveDepthSearch : IBestMoveChecker
    {

        private List<List<ISquarePiece>> bestMove = new List<List<ISquarePiece>>();
        private IRestriction restriction;
            
        private DateTime startTime; 

        private int MinAllowed
        {
            get
            {
                if (restriction is MinSequenceLimit)
                {
                    return (restriction as MinSequenceLimit).MinLimit;
                }
                return 2;
            }
        }

        private int MaxAllowed
        {
            get
            {
                if (restriction is MaxSequenceLimit)
                {
                    return (restriction as MaxSequenceLimit).MaxLimit;
                }
                return int.MaxValue;
            }
        }

        private bool TimeExpired
        {
            get
            {
                return (DateTime.Now - startTime).TotalSeconds > Constants.GameSettings.BestMoveSearchTimeOut;
            }
        }

        public (SearchResult Result, List<ISquarePiece> Move) GetBestMove()
        {
            return GetBestMove(new NoRestriction());
        }

        public (SearchResult Result, List<ISquarePiece> Move) GetBestMove(IRestriction restriction)
        {
            this.restriction = restriction;
            startTime = DateTime.Now;

            IScoreCalculator scoreCalculator = new StandardScoreCalculator();
            
            bestMove = new List<List<ISquarePiece>>();
            for (int x = 0; x < PieceManager.Instance.NumberOfColumns; x++)
            {
                for (int y = 0; y < PieceManager.Instance.NumberOfRows; y++)
                {
                    var visited = new List<ISquarePiece>();
                    var currentPath = new List<ISquarePiece>();

                    var piece = PieceManager.Instance.GetPiece(x, y);
                    if (piece == null)
                    {
                        continue;
                    }

                    visited.Add(piece);
                    currentPath.Add(piece);
                    ExplorePiece(piece, visited, currentPath);
                }

                if (TimeExpired)
                {
                    return (SearchResult.TimeOut, new List<ISquarePiece>());
                }
            }

            var filtered = bestMove.Where(x => x.Count >= MinAllowed && !restriction.IsRestrictionViolated(x.ToArray()));
            if (filtered.Count() == 0)
            {
                return (SearchResult.NoMoves, new List<ISquarePiece>());
            }

            var best = filtered.OrderByDescending(x => scoreCalculator.CalculateScore(x.ToArray())).First();
            
        
            Debug.Log(string.Join(", ", best.Select(x => x.Position.ToString())));
            return (SearchResult.Success, best.ToList());
        }

        private void ExplorePiece(ISquarePiece piece, List<ISquarePiece> currentPath, List<ISquarePiece> visited)
        {
            if (TimeExpired)
            {
                return;
            }

            var neighbours = AIHelpers.GetNeighbours(restriction, piece);

            bool executed = false;
            while (neighbours.Count > 0 && currentPath.Count < MaxAllowed)
            {
                var nextPiece = neighbours.Pop();
                if (currentPath.Contains(nextPiece))
                {
                    continue;
                }
                else
                {
                    executed = true;
                    visited.Add(nextPiece);
                    currentPath.Add(nextPiece);
                }
                ExplorePiece(nextPiece, currentPath, visited);
            }               

            if(!executed)
            {
                bestMove.Add(currentPath.Select(x => x).ToList());
            }
            currentPath.Remove(piece);
        }

      

    }
}
