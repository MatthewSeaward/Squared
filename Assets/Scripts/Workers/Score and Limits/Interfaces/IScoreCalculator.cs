using System.Collections.Generic;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public interface IScoreCalculator
    {
        int CalculateScore(ISquarePiece[] sequence);
    }
}