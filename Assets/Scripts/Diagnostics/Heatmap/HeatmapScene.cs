using Assets.Scripts.Workers.IO;
using Assets.Scripts.Workers.IO.Heatmap;
using Assets.Scripts.Workers.IO.Score;
using Assets.Scripts.Workers.IO.Data_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class HeatmapScene : MonoBehaviour
    {
        [SerializeField]
        private GameObject square;

        [SerializeField]
        private Text Text;

        [SerializeField]
        private Text star1, star2, star3, average;

        [SerializeField]
        InputField chapterField;

        private IHeatMapReader heatMapReader = new FireBaseHeatmapReader();
        private IScoreReader scoreReader = new FireBaseScoreReader();

        private Dictionary<string, int> data;
        private List<ScoreEntry> scores;

        bool drawn = true, drawnScores = true;
        int selectedLevel = 0;
        string chapter = "Golem";
        List<GameObject> squares = new List<GameObject>();

        private void Start()
        {
            FireBaseHeatmapReader.HeatmapLoaded += HeatmapLoaded;
            FireBaseScoreReader.ScoresLoaded += ScoresLaodedHandler;

            for (int y = 0; y < 11; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    var go = Instantiate(square, new Vector3(x, y * -1), new Quaternion(0, 0, 0, 0));

                    var key = $"{x}:{y}";
                    go.name = key;

                    squares.Add(go);
                }
            }
            Refresh();
        }

        private void ScoresLaodedHandler(List<ScoreEntry> scores)
        {
            this.scores = scores;
            drawnScores = false;
        }

        public void NextLevel()
        {
            selectedLevel = Mathf.Clamp(selectedLevel + 1, 0, 12);
            Refresh();
        }

        public void PreviousLevel()
        {
            selectedLevel = Mathf.Clamp(selectedLevel - 1, 0, 12);
            Refresh();
        }

        public void ChapterChanged(string value)
        {
            chapter = chapterField.text;

            Refresh();
        }

        private void Refresh()
        {
            Text.text = selectedLevel.ToString();

            Clear();
            heatMapReader.GetHeatmapAsync(chapter, selectedLevel);
            scoreReader.GetScoresAsync(chapter, selectedLevel);

        }

        private void RefreshScores(List<ScoreEntry> scores)
        {          
            AddScores(star1, 1, scores);
            AddScores(star2, 2, scores);
            AddScores(star3, 3, scores);
            AddAverage(average, scores);
        }

        private void AddScores(Text text, int star, List<ScoreEntry> scores)
        {
            text.text = "Star " + star;

            int total = 0;
            var filteredScores = scores.Where(x => x.Star == star);

            if (filteredScores.Count() > 0)
            {
                foreach (var score in filteredScores.OrderBy(x => x.Score))
                {
                    text.text += Environment.NewLine + $"{score.Score} ({score.Result.ToString()})";
                    total += score.Score;
                }

                text.text += Environment.NewLine + "Avg: " + (total / filteredScores.Count());
            }
        }

        private void AddAverage(Text text, List<ScoreEntry> scores)
        {
            int total = 0;

            foreach(var list in scores)
            {
                total += list.Score;
            }

            text.text = "Average";

            if (scores.Count > 0)
                text.text += Environment.NewLine + (total / scores.Count());
        }

        private void Update()
        {
            if (!drawn)
            {

                Clear();

                var largestNumber = data.Values.Max();

                float gradientAmount = (float)1 / (float)largestNumber;

                foreach (var item in data)
                {
                    var go = squares.FirstOrDefault(x => x.name == item.Key);
                    if (go == null)
                    {
                        go.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    else
                    {
                        var newColour = item.Value * gradientAmount;
                        go.GetComponent<SpriteRenderer>().color = new Color(newColour, 0, 0);
                    }
                }

                drawn = true;
            }

            if (!drawnScores)
            {
                RefreshScores(scores);
                drawnScores = true;
            }
        }

        private void Clear()
        {
            squares.ForEach(x => x.GetComponent<SpriteRenderer>().color = Color.white);
        }

        private void HeatmapLoaded(Dictionary<string, int> data)
        {
            this.data = data;
            drawn = false;
        }
    }
}
