using Assets.Scripts.Managers;
using Assets.Scripts.UI;
using Assets.Scripts.Workers.Generic_Interfaces;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public delegate void LimitReached();

    class LimitDisplay : MonoBehaviour
    {
        public static event LimitReached LimitReached;

        [SerializeField]
        private Text Limit;

        [SerializeField]
        private ProgressBar LimitProgress;

        private IGameLimit _gameLimit;
        private IUpdateable _updateableLimit;

        private void Start()
        {
            _gameLimit = GameManager.Instance.GameLimit;
            _gameLimit.Reset();

            _updateableLimit = _gameLimit as IUpdateable;

            if (LevelManager.Instance.DailyChallenge)
            {
                LimitProgress.gameObject.SetActive(false);
            }

            UpdateLimit(0);
        }

        private void Update()
        {
            if (GameManager.Instance.GamePaused || MenuProvider.Instance.OnDisplay)
            {
                return;
            }

            UpdateLimit(Time.deltaTime);
        }

        private void UpdateLimit(float deltaTime)
        {
            _updateableLimit?.Update(deltaTime);

            Limit.text = _gameLimit.GetLimitText();

            if (!LevelManager.Instance.DailyChallenge)
            {
                LimitProgress.UpdateProgressBar(_gameLimit.PercentComplete());
            }

            if (_gameLimit.ReachedLimit())
            {
                LimitReached?.Invoke();
            }
        }        

        internal void ActivateLimit()
        {
            Limit.GetComponent<Animator>().SetTrigger("Activate");
        }
    }
}
