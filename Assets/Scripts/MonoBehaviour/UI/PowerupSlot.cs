using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;
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
        protected int previousRemaing;
        private bool activateOnEnable;


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

        public void Setup(IPowerup powerup)
        {
            this.powerup = powerup;

            Icon.sprite = powerup.Icon;
            if (button != null)
            {
                button.onClick.AddListener(() => OnButtonClicked());
            }
            previousRemaing = UserPowerupManager.Instance.GetUses(powerup);
            UpdateRemainingText(powerup);
        }

        private void UpdateRemainingText(IPowerup powerup)
        {
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
            if (button != null)
            {
                button.interactable = EnableButton(remaining);
            }
            previousRemaing = remaining;
        }

        private void Update()
        {
            ChildUpdate(Time.deltaTime);
        }

        protected virtual void ChildUpdate(float deltaTime)
        {

        }

        protected abstract bool EnableButton(int remaining);

        protected abstract void OnButtonClicked(); 

    }
}
