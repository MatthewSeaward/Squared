using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mono_Behaviour.UI.Components
{
    class ScoreEntry : MonoBehaviour
    {
        [SerializeField]
        private Text Rank;

        [SerializeField]
        private Text Name;

        [SerializeField]
        private Text Score;

        public void Setup(int rank, string name, int score)
        {
            Rank.text = rank.ToString();
            Name.text = name;
            Score.text = score.ToString();
        }

        internal void SetText(string text)
        {
            Rank.text = string.Empty;
            Name.text = text;
            Score.text = string.Empty;
        }
    }
}
