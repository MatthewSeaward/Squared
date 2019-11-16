using Assets.Scripts.Managers;
using Assets.Scripts.UI;
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

        private IGameLimit GameLimit;

        private void Start()
        {
            GameLimit = GameManager.Instance.GameLimit;
            GameLimit.Reset();

            UpdateLimit(0);
        }

        private void Update()
        {
            if (GameManager.Instance.GamePaused || MenuProvider.Instance.OnDisplay)
            {
                return;
            }

            UpdateLimit(UnityEngine.Time.deltaTime);
        }

        private void UpdateLimit(float deltaTime)
        {
            GameLimit.Update(deltaTime);

            Limit.text = GameLimit.GetLimitText();

            LimitProgress.UpdateProgressBar(GameLimit.PercentComplete());

            if (GameLimit.ReachedLimit())
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
