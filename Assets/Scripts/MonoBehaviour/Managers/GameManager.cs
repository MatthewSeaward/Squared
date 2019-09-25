using UnityEngine;

public delegate void PauseStateChanged(bool paused);

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static PauseStateChanged PauseStateChanged;

        private bool _gamePaused;

        public bool GamePaused
        {
            get
            {
                return _gamePaused;
            }
            set
            {
                if (_gamePaused == value)
                {
                    return;
                }

                _gamePaused = value;
                PauseStateChanged?.Invoke(_gamePaused);
            }
        }

        public bool GameOver { get; private set; } = false;

        public static GameManager Instance { get; private set; }

        void Awake()
        {
            Instance = this;
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
