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

        private void OnEnable()
        {
            UpdateText(LivesManager.LivesRemaining);
        }

        private void UpdateText(int lives)
        {
            remaining.text = $"X{lives}";
        }
    }
}