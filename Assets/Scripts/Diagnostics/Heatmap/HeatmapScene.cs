using Assets.Scripts.Workers.IO.Heatmap;
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
        InputField chapterField;

        private IHeatMapReader heatMapReader = new FireBaseHeatmapReader();
        private Dictionary<string, int> data;
        bool drawn = true;
        int selectedLevel = 0;
        string chapter = "Golem";
        List<GameObject> squares = new List<GameObject>();

        private void Start()
        {
            FireBaseHeatmapReader.HeatmapLoaded += HeatmapLoaded;

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
            var data = heatMapReader.GetHeatmap(chapter, selectedLevel);
        }

        private void Update()
        {
            if (drawn) return;

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
