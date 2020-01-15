using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Collections.Generic;
using UnityEngine;

public delegate void PauseStateChanged(bool paused);

namespace Assets.Scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static PauseStateChanged PauseStateChanged;

        private List<object> lockers  = new List<object>();

        private bool _gamePaused;

        public bool GamePaused
        {
            get
            {
                return _gamePaused;
            }
            private set
            {
                if (_gamePaused == value)
                {
                    return;
                }

                _gamePaused = value;
                PauseStateChanged?.Invoke(_gamePaused);
            }
        }

        public bool GameOver { get; set; } = false;

        public static GameManager Instance { get; private set; }

        public IRestriction Restriction;
        public IGameLimit GameLimit;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            var currentLevel = LevelManager.Instance.GetNextLevel();
            Restriction = currentLevel.GetCurrentRestriction();
            GameLimit = currentLevel.GetCurrentLimit();
        }

        public void ChangePauseState(object locker, bool pauseState)
        {
            if (pauseState)
            {
                GamePaused = true;
                if (!lockers.Contains(locker))
                {
                    lockers.Add(locker);
                }
            }
            else
            {
                if (lockers.Contains(locker))
                {
                    lockers.Remove(locker);
                }
                if (lockers.Count == 0)
                {
                    GamePaused = false;
                }
            }
        }
    }
}
