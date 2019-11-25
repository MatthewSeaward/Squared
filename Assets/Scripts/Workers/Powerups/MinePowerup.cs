using Assets.Scripts.Managers;
using Assets.Scripts.UI;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Selection;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    public class MinePowerup : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Mine"];
        
        public string Name => "Mine";

        public string Description => "Quickly tap on a few pieces to destroy them instantly.";

        public bool Enabled => true;

        private float TimeToPreformAction = 5f;

        public void Invoke()
        {
            PieceSelectionManager.Instance.PieceSelection = new PieceSelectionModeDestroyTap();
            CountdownPanel.TimerElapsed += CountdownPanel_TimerElapsed;
            CountdownPanel.Instance.StartCountDown(TimeToPreformAction);
            GameManager.Instance.ChangePauseState(this, true);
        }

        private void CountdownPanel_TimerElapsed()
        {
            CountdownPanel.TimerElapsed -= CountdownPanel_TimerElapsed;
            GameManager.Instance.ChangePauseState(this, false);
            PieceSelectionManager.Instance.ReturnPieceSelectionModeToDefault();        
        }

        public void Update(float deltaTime)
        {
        }

        public void MoveCompleted()
        {
        }
    }
}
