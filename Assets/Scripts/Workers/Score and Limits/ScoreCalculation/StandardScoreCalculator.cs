using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class StandardScoreCalculator : IScoreCalculator
    {
        public int CalculateScore(LinkedList<ISquarePiece> pieces)
        {
            int totalScore = 0;
            foreach(ISquarePiece piece in pieces.OrderBy(x => x.Scoring.Prority))
            {
                totalScore = piece.Scoring.ScorePiece(totalScore);
            }

            var bonus = Constants.BonusPoints.Points.OrderByDescending(x => x.Item1).FirstOrDefault(x => pieces.Count >= x.Item1);

            return totalScore + bonus.Item2;
        }
    }
}
