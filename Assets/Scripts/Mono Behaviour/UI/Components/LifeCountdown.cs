using Assets.Scripts.Workers.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mono_Behaviour.UI.Components
{
    class LifeCountdown : MonoBehaviour
    {
        public static readonly Color depletedColour = new Color(1f, 1f, 1f, 0.4f);

        [SerializeField]
        private bool HideIfLivesFull = true;

        private Text countdown;

        private void Awake()
        {
            LivesManager.LivesChanged += LivesManager_LivesChanged;
        }

        private void OnDestroy()
        {
            LivesManager.LivesChanged -= LivesManager_LivesChanged;
        }

        private void LivesManager_LivesChanged(bool gained, int newLives)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => OnEnable());
        }

        private void OnEnable()
        {
            if (HideIfLivesFull)
            {
                SetChildrenEnabled(LivesManager.Instance.LivesRemaining == 0);
            }
            else
            {
                SetChildrenEnabled(true);
            }

            countdown = GetComponentInChildren<Text>();
            var image = GetComponentInChildren<Image>();
            image.color = LivesManager.Instance.LivesRemaining == 0 ? depletedColour : Color.white;
        }

        private void Update()
        {
            if (countdown == null)
            {
                return;
            }

            var remainingTime = LivesManager.Instance.LastEarnedLife.AddMinutes(RemoteConfigHelper.GetLivesRefreshTime()) - DateTime.Now;

            if (remainingTime.TotalMilliseconds < 0)
            {
                remainingTime = new TimeSpan(0, 0, 0);
            }

            UpdateText(remainingTime);
        }

        private void UpdateText(TimeSpan remainingTime)
        {
            if (LivesManager.Instance.LivesRemaining == RemoteConfigHelper.GetMaxLives())
            {
                countdown.text = "At maximum credits";
            }
            else
            {
                var livesLeft = LivesManager.Instance.LivesRemaining == 0 ? $"No credits remaining{Environment.NewLine}" : string.Empty;
                countdown.text = $"{livesLeft}Next credit in {remainingTime.Minutes} minute(s) and {remainingTime.Seconds} second(s).";
            }
        }

        private void SetChildrenEnabled(bool enabled)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(enabled);
            }
        }
    }
}