namespace Assets.Scripts.Workers.Score_and_Limits.Interfaces
{
    public interface IScoreCalculator
    {
        string ActiveBonus { get; }
        int CalculateScore(ISquarePiece[] sequence);
    }
}