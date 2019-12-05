using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO.Data_Entities;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mono_Behaviour.UI.Components
{
    class LifeCountdown : MonoBehaviour
    {
        [SerializeField]
        private bool HideIfNoLives = true;

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
            if (HideIfNoLives)
            {
                SetChildrenEnabled(LivesManager.Instance.LivesRemaining == 0);
            }
            else
            {
                SetChildrenEnabled(true);
            }

            countdown = GetComponentInChildren<Text>();
            var image = GetComponentInChildren<Image>();
            image.overrideSprite = LivesManager.Instance.LivesRemaining == 0 ? GameResources.Sprites["heart broken"] : GameResources.Sprites["heart big"];
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
                countdown.text = "At maximum lives";
            }
            else
            {
                var livesLeft = LivesManager.Instance.LivesRemaining == 0 ? $"No lives remaining{Environment.NewLine}" : string.Empty;
                countdown.text = $"{livesLeft}Next life in {remainingTime.Minutes} minute(s) and {remainingTime.Seconds} second(s).";
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