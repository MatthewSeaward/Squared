using System.Collections.Generic;

namespace Assets.Scripts.Workers.IO.Score
{
    public interface IScoreReader
    {
        Dictionary<int, List<int>> GetScores(string chapter, int level);
        void GetScoresAsync(string chapter, int level);

    }
}
