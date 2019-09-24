using System;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public bool GameOver { get; private set; } = false;

        private static GameManager _instance;

        public static GameManager Instance => _instance;

        void Awake()
        {
            _instance = this;
        }

        void Start()
        {
            ScoreKeeper.GameCompleted += ScoreKeeper_GameCompleted;
        }

        void OnDestroy()
        {
            ScoreKeeper.GameCompleted -= ScoreKeeper_GameCompleted;
        }

        private void ScoreKeeper_GameCompleted(string chapter, int level, int star, int score, GameResult result)
        {
            GameOver = true;
        }
    }
}
