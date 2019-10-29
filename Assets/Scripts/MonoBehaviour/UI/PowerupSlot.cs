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
        }

        private void OnDestroy()
        {
            PieceSelectionManager.SequenceCompleted -= PieceSelectionManager_SequenceCompleted;
        }

        public void Setup(IPowerup powerup)
        {
            this.powerup = powerup;
            Icon.sprite = powerup.Icon;
            button.onClick.AddListener(() => PowerupInvoke());
            UpdateRemainingText();
        }

        private void UpdateRemainingText()
        {
            int remaining = UserPowerupManager.GetUses(powerup);
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
            UserPowerupManager.UsePowerup(powerup);
            UpdateRemainingText();
            button.interactable = false;
        }

        private void PieceSelectionManager_SequenceCompleted(ISquarePiece[] pieces)
        {
            button.interactable = UserPowerupManager.GetUses(powerup) > 0;
        }
    }
}
