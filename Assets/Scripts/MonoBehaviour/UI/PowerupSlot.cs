using System;
using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    class PowerupSlot : MonoBehaviour
    {
        [SerializeField]
        private Image Icon;

        [SerializeField]
        private Button button;

        [SerializeField]
        private Text remainingText;

        private IPowerup powerup;

        private void Start()
        {
            PieceSelectionManager.SequenceCompleted += PieceSelectionManager_SequenceCompleted;
            UserPowerupManager.PowerupCountChanged += UpdateRemainingText;
        }
        private void OnDestroy()
        {
            PieceSelectionManager.SequenceCompleted -= PieceSelectionManager_SequenceCompleted;
            UserPowerupManager.PowerupCountChanged -= UpdateRemainingText;
         }

        public void Setup(IPowerup powerup)
        {
            this.powerup = powerup;
            Icon.sprite = powerup.Icon;
            button.onClick.AddListener(() => PowerupInvoke());
            UpdateRemainingText(powerup);
        }

        private void UpdateRemainingText(IPowerup powerup)
        {
            if (powerup.GetType() != this.powerup.GetType())
            {
                return;
            }

            int remaining = UserPowerupManager.Instance.GetUses(powerup);
            remainingText.text = remaining.ToString();
            button.interactable = remaining > 0;
        }

        private void Update()
        {
            powerup.Update(Time.deltaTime);
        }

        private void PowerupInvoke()
        {
            powerup.Invoke();
            UserPowerupManager.Instance.UsePowerup(powerup);
            button.interactable = false;
        }

        private void PieceSelectionManager_SequenceCompleted(ISquarePiece[] pieces)
        {
            button.interactable = UserPowerupManager.Instance.GetUses(powerup) > 0;
        }
    }
}
