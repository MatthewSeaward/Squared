using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Score;
using Assets.Scripts.Workers.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mono_Behaviour.UI.Game
{
    class ScoreSummaryPanel : MonoBehaviour
    {
        [SerializeField]
        private Text ScoreSummary;

        private void OnEnable()
        {
            ScoreSummary.text = string.Empty;

            FireBaseScoreReader.DailyScoresLoaded += PopulateDailyScores;

            var dataloader = new FireBaseScoreReader();
            dataloader.GetDailyScoresAsync();
        }

        private void PopulateDailyScores(List<ScoreEntry> scores)
        {
            FireBaseScoreReader.DailyScoresLoaded -= PopulateDailyScores;
            UnityMainThreadDispatcher.Instance().Enqueue(() => DisplayDailyScores(scores));
        }

        private void DisplayDailyScores(List<ScoreEntry> scores)
        {
            var worldHighest = scores
                            .Select(x => x.Score)
                            .OrderByDescending(x => x)
                            .FirstOrDefault();

            var myHighest = scores
                                .Where(x => x.User == UserManager.UserID)        
                                .Select(x => x.Score)
                                .OrderByDescending(x => x)
                                .FirstOrDefault();

            ScoreSummary.text = "World Best: " + (worldHighest > 0 ? worldHighest.ToString() : "Unset") + Environment.NewLine;
            ScoreSummary.text += "Your Best: " + (myHighest > 0 ? myHighest.ToString() : "Unset");
        }

        public void ShowHighScores()
        {
            MenuProvider.Instance.ShowHighScore();
        }
    }
}
