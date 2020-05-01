using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Score;
using Assets.Scripts.Workers.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Mono_Behaviour.UI.Game
{
    class HighScoreScreen : MonoBehaviour
    {
        private Components.ScoreEntry[] scoreEntries;

        private bool WorldScores = true;
        private bool Loading = false;

        private IScoreReader dataReader = new FireBaseScoreReader();

        private void OnEnable()
        {
            scoreEntries = GetComponentsInChildren<Components.ScoreEntry>(true);

            FireBaseScoreReader.DailyScoresLoaded += PopulateDailyScores;

            Refresh();
        }

        private void OnDisable()
        {
           FireBaseScoreReader.DailyScoresLoaded -= PopulateDailyScores;
        }

        public void WorldScoresClicked()
        {
            WorldScores = true;
            Refresh();
        }

        public void OwnScoresClicked()
        {
            WorldScores = false;
            Refresh();
        }

        private void Refresh()
        {
            if (Loading)
            {
                return;
            }

            Loading = true;

            dataReader.GetDailyScoresAsync();
        }

        private void PopulateDailyScores(List<ScoreEntry> scores)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => DisplayDailyScores(scores));
        }

        private void DisplayDailyScores(List<ScoreEntry> scores)
        {
            List<ScoreEntry> filteredScores = null;

            for (int i = 0; i < scoreEntries.Length; i++)
            {
                scoreEntries[i].gameObject.SetActive(false);
            }

            if (WorldScores)
            {
                filteredScores = scores
                                .OrderByDescending(x => x.Score)
                                .ToList();
            }
            else
            {
                filteredScores = scores
                              .Where(x => x.User == UserManager.UserID)
                              .OrderByDescending(x => x.Score)
                              .ToList();
            }

            if (!filteredScores.Any())
            {
                scoreEntries[0].gameObject.SetActive(true);
                scoreEntries[0].SetText("No Data");
            }
            else
            {
                for (int i = 0; i < scoreEntries.Length; i++)
                {
                    if (i >= filteredScores.Count())
                    {
                        scoreEntries[i].gameObject.SetActive(false);
                    }
                    else
                    {
                        scoreEntries[i].gameObject.SetActive(true);
                        scoreEntries[i].Setup(i + 1, filteredScores[i].User, filteredScores[i].Score);
                    }
                }
            }
            Loading = false;
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
