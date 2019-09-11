namespace Assets.Scripts.Workers.Piece_Effects.Interfaces
{
    public enum Priority { Low, High }

    public interface IScoreable
    {
        Priority Prority { get; }
        int ScorePiece(int currentScore);
    }
}