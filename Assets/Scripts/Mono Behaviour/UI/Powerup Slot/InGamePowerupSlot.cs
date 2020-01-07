using Assets.Scripts.Managers;
using Assets.Scripts.Workers.Managers;

namespace Assets.Scripts.UI
{
    public class InGamePowerupSlot : PowerupSlot
    {
        protected override void ChildStart()
        {
            PieceSelectionManager.MoveCompleted += PieceSelectionManager_MoveCompleted;
            GameManager.PauseStateChanged += GameManager_PauseStateChanged;
        }

        protected override void ChildOnDestroy()
        {
            PieceSelectionManager.MoveCompleted -= PieceSelectionManager_MoveCompleted;

            GameManager.PauseStateChanged -= GameManager_PauseStateChanged;
        }

        protected override void ChildUpdate(float deltaTime)
        {
            UpdateablePowerup?.Update(deltaTime);
        }
   
        private void PieceSelectionManager_MoveCompleted()
        {
            button.interactable = UserPowerupManager.Instance.GetUses(powerup) > 0 && powerup.Enabled;
            powerup.MoveCompleted();
        }

        protected override void OnButtonClicked()
        {
            if (GameManager.Instance.GamePaused || MenuProvider.Instance.OnDisplay)
            {
                return;
            }

            button.interactable = false;
            powerup.Invoke();
            UserPowerupManager.Instance.UsePowerup(powerup);
        }

        protected override bool EnableButton(int remaining)
        {
            if (GameManager.Instance.GamePaused || MenuProvider.Instance.OnDisplay)
            {
                return false;
            }

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
