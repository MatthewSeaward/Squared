using System.Collections.Generic;

namespace Assets.Scripts.Workers.IO.Score
{
    class DummyScoreReader : IScoreReader
    {
        public Dictionary<int, List<int>> GetScores(string chapter, int level)
        {
            var scores1 = new List<int>
            {
                UnityEngine.Random.Range(70, 150),UnityEngine.Random.Range(70, 150),UnityEngine.Random.Range(70, 150),UnityEngine.Random.Range(70, 150),UnityEngine.Random.Range(70, 150),
                UnityEngine.Random.Range(70, 150),UnityEngine.Random.Range(70, 150),UnityEngine.Random.Range(70, 150),UnityEngine.Random.Range(70, 150)
            };

            var scores2 = new List<int>
            {
                UnityEngine.Random.Range(60, 120),UnityEngine.Random.Range(60, 120),UnityEngine.Random.Range(60, 120),UnityEngine.Random.Range(60, 120),UnityEngine.Random.Range(60, 120),
                UnityEngine.Random.Range(60, 120),UnityEngine.Random.Range(60, 120),UnityEngine.Random.Range(60, 120),UnityEngine.Random.Range(60, 120)
            };

            var scores3 = new List<int>
            {
                 UnityEngine.Random.Range(50, 160),UnityEngine.Random.Range(50, 160),UnityEngine.Random.Range(50, 160),UnityEngine.Random.Range(50, 160),UnityEngine.Random.Range(50, 160)
            };

            return new Dictionary<int, List<int>>
            {
                { 1, scores1},
                { 2, scores2},
                { 3, scores3}
            };
            
        }

        public void GetScoresAsync(string chapter, int level)
        {
            throw new System.NotImplementedException();
        }
    }
}
