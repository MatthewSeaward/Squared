using Assets.Scripts.Workers.Powerups;
using Assets.Scripts.Workers.Score_and_Limits.ScoreCalculation;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class StandardScoreCalculator : IScoreCalculator
    {
        private List<ScoreBonus> CurrentBonuses = new List<ScoreBonus>();

        public string ActiveBonus
        {
            get
            {
                float total = 0;
                foreach(var b in CurrentBonuses)
                {
                    if (!b.Active)
                    {
                        continue;
                    }
                    total += b.Multiplier;
                }

                if (total > 0)
                {
                    return total.ToString("0.00");
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public StandardScoreCalculator()
        {
            ExtraPoints.BonusAdded += AddNewBonus;
        }

        ~StandardScoreCalculator()
        {
            ExtraPoints.BonusAdded -= AddNewBonus;
        }

        public int CalculateScore(ISquarePiece[] sequence)
        {
            int totalScore = 0;
            foreach(ISquarePiece piece in sequence.OrderBy(x => x.Scoring.Prority))
            {
                totalScore = piece.Scoring.ScorePiece(totalScore);
            }

            if (Constants.BonusPoints.Points != null)
            {
                var bonus = Constants.BonusPoints.Points.OrderByDescending(x => x.Item1).FirstOrDefault(x => sequence.Length >= x.Item1);

                totalScore += bonus.Item2;
            }

            float mulltiplier = 0f;
            foreach (var b in CurrentBonuses)
            {
                if (!b.Active)
                {
                    continue;
                }
                mulltiplier += b.Multiplier;
                b.TurnPast();
            }

            if (mulltiplier >0)
            {
                totalScore = (int)(totalScore * mulltiplier);
            }

            return totalScore;
        }

        public void AddNewBonus(ScoreBonus bonus)
        {
            CurrentBonuses.Add(bonus);
        }
    }
}
