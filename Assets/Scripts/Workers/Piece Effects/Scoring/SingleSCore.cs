using Assets.Scripts.Workers.Piece_Effects.Interfaces;

namespace Assets.Scripts.Workers.Piece_Effects
{
    public class SingleScore : IScoreable, ITextLayer
    {
        private int scoreValue;

        public Priority Prority => Priority.Low;

        public SingleScore(int scoreValue = 1)
        {
            this.scoreValue = scoreValue;
        }

        public int ScorePiece(int currentScore)
        {
            return currentScore + scoreValue;
        }

        public string GetText()
        {
            if (scoreValue > 1)
            {
                return scoreValue.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
