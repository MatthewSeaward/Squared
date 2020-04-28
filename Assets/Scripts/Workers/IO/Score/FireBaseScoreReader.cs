using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.Scripts.Workers.IO.Score
{

    public delegate void ScoresLoaded(List<LevelScoreEntry> scores);
    public delegate void DailyScoresLoaded(List<ScoreEntry> scores);

    class FireBaseScoreReader : IScoreReader
    {
        public static ScoresLoaded ScoresLoaded;
        public static DailyScoresLoaded DailyScoresLoaded;

        public void GetDailyScoresAsync()
        {
            try
            {
                var t = new Task(() =>
                {
                    var result = FireBaseReader.ReadSingleAsync<ScoreEntry>(FireBaseSavePaths.DailyChallengeLocation(DateTime.Now));
                    DailyScoresLoaded?.Invoke(result.Result);
                });
                t.Start();
            }
            catch (Exception ex)
            {
                DebugLogger.Instance.WriteException(ex);
            }
        }

        public Dictionary<int, List<int>> GetScores(string chapter, int level)
        {
            throw new NotImplementedException();
        }

        public void GetScoresAsync(string chapter, int level)
        {
            try
            {
                var t = new Task(() =>
                {
                    var result = FireBaseReader.ReadAsync<LevelScoreEntry>(FireBaseSavePaths.ScoreLocation(chapter, level + 1));
                    ScoresLoaded?.Invoke(result.Result);
                });
                t.Start();

            }
            catch (Exception ex)
            {
                DebugLogger.Instance.WriteException(ex);
            }
        }
    }
}
