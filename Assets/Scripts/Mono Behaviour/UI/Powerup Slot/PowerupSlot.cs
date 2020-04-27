using Assets.Scripts.Constants;
using Assets.Scripts.Workers.Generic_Interfaces;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Powerups.Interfaces;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public abstract class PowerupSlot : MonoBehaviour
    {
        [SerializeField]
        protected Image Icon;

        [SerializeField]
        public Button button;

        [SerializeField]
        protected Text remainingText;

        public IPowerup powerup;
        protected IUpdateable UpdateablePowerup => powerup as IUpdateable;

        protected int previousRemaing;
        private bool activateOnEnable;

        private bool InGame => SceneManager.GetActiveScene().name == Scenes.Game;

        private void Start()
        {
            UserPowerupManager.PowerupCountChanged += UpdateRemainingText;
            ChildStart();
        }
        private void OnDestroy()
        {
            UserPowerupManager.PowerupCountChanged -= UpdateRemainingText;
            ChildOnDestroy();
        }

        protected virtual void ChildStart()
        {

        }

        protected virtual void ChildOnDestroy()
        {

        }

        private void OnEnable()
        {
            if (activateOnEnable)
            {
                GetComponent<Animator>().SetTrigger("Activate");
            }
            activateOnEnable = false;
        }

        public virtual void Setup(IPowerup powerup, bool enableClick = true)
        {
            if (LevelManager.Instance.DailyChallenge)
            {
                return;
            }

            this.powerup = powerup;

            Icon.sprite = powerup.Icon;
            if (button != null && enableClick)
            {
                button.onClick.AddListener(() => OnButtonClicked());
            }

            previousRemaing = UserPowerupManager.Instance.GetUses(powerup);
            UpdateRemainingText(powerup);
        }

        private void UpdateRemainingText(IPowerup powerup)
        {
            if (LevelManager.Instance.DailyChallenge)
            {
                return;
            }

            if (powerup.GetType() != this.powerup.GetType())
            {
                return;
            }

            int remaining = UserPowerupManager.Instance.GetUses(powerup);

            if (remaining > previousRemaing)
            {
                activateOnEnable = true;
            }

            remainingText.text = remaining.ToString();
            previousRemaing = remaining;
        }

        private void Update()
        {
            if (!InGame)
            {
                return;
            }

            ChildUpdate(Time.deltaTime);
        }

        protected virtual void ChildUpdate(float deltaTime)
        {

        }

        protected abstract bool EnableButton(int remaining);

        protected abstract void OnButtonClicked(); 

    }
}
