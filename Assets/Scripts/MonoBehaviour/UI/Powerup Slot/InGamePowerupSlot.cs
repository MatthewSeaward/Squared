using Assets.Scripts.Managers;
using Assets.Scripts.Workers.Data_Managers;

namespace Assets.Scripts.UI
{
    public class InGamePowerupSlot : PowerupSlot
    {

        protected override void ChildStart()
        {
            PieceSelectionManager.SequenceCompleted += PieceSelectionManager_SequenceCompleted;
            GameManager.PauseStateChanged += GameManager_PauseStateChanged;
        }
              
        protected override void ChildOnDestroy()
        {
            PieceSelectionManager.SequenceCompleted -= PieceSelectionManager_SequenceCompleted;
            GameManager.PauseStateChanged += GameManager_PauseStateChanged;
        }

        protected override void ChildUpdate(float deltaTime)
        {
            powerup.Update(deltaTime);
        }
        
        private void PieceSelectionManager_SequenceCompleted(ISquarePiece[] pieces)
        {
            button.interactable = UserPowerupManager.Instance.GetUses(powerup) > 0 && powerup.Enabled;
        }

        protected override void OnButtonClicked()
        {
            powerup.Invoke();
            UserPowerupManager.Instance.UsePowerup(powerup);
            button.interactable = false;
        }

        protected override bool EnableButton(int remaining)
        {
            return remaining > 0 && powerup.Enabled;
        }

        private void GameManager_PauseStateChanged(bool paused)
        {
            if (paused)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = EnableButton(previousRemaing);
            }
        }

    }
}
