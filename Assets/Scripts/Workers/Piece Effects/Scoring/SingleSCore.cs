using Assets.Scripts.Workers.Piece_Effects.Interfaces;

namespace Assets.Scripts.Workers.Piece_Effects
{
    public class SingleScore : IScoreable
    {
        public int ScorePiece(int currentScore)
        {
            return currentScore + 1;
        }
    }
}
