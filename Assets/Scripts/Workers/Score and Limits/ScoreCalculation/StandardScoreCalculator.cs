using System.Collections.Generic;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class StandardScoreCalculator : IScoreCalculator
    {
        public int CalculateScore(LinkedList<ISquarePiece> pieces)
        {
            int totalScore = 0;
            foreach(SquarePiece piece in pieces)
            {
                totalScore = piece.Scoring.ScorePiece(totalScore);
            }

            return totalScore;
        }
    }
}
