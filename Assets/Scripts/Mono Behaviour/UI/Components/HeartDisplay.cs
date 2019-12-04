using Assets.Scripts.Workers.Data_Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class HeartDisplay : MonoBehaviour
    {
        private Text remaining;
        private Animator animator;

        private void Awake()
        {
            remaining = GetComponentInChildren<Text>();
            animator = GetComponentInChildren<Animator>();

            LivesManager.LivesChanged += LivesManager_LivesChanged;
        }

        private void OnDestroy()
        {
            LivesManager.LivesChanged -= LivesManager_LivesChanged;
        }

        private void LivesManager_LivesChanged(bool gained, int newLives)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => UpdateHeartDisplay(gained, newLives));
        }

        private void UpdateHeartDisplay(bool gained, int newLives)
        {
            try
            {
                UpdateText(newLives);

                if (gained)
                {
                    animator.SetTrigger("Heart Gain");
                }
                else
                {
                    animator.SetTrigger("Heart Lose");
                }
            }
            catch (System.Exception ex)
            {
                Workers.Helpers.DebugLogger.Instance.WriteException(ex);
            }
        }

        private void OnEnable()
        {
            UpdateText(LivesManager.Instance.LivesRemaining);
        }

        private void UpdateText(int lives)
        {
            remaining.text = $"X{lives}";
        }
    }
}