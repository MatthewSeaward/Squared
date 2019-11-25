using Assets.Scripts.Managers;
using Assets.Scripts.UI;
using Assets.Scripts.Workers.Grid_Management;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class PerformBestMove : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Crown"];
        
        public string Name => "Best Move";

        public string Description => "Instantly preforms the best move.";

        public bool Enabled => true;

        private IBestMoveChecker bestMoveChecker = new BestMoveDepthSearch();

        public void Invoke()
        {
            var move = bestMoveChecker.GetBestMove(LevelManager.Instance.SelectedLevel.GetCurrentRestriction());
            if (move == null || move.Count == 0)
            {
                return;
            }
            GameManager.Instance.ChangePauseState(this, true);
            ToastPanel.Instance.ShowText("Thinking");

            PieceSelectionManager.Instance.PreformMove(move);
        }

        public void Update(float deltaTime)
        {
        }

        public void MoveCompleted()
        {
            GameManager.Instance.ChangePauseState(this, false);
            ToastPanel.Instance.Hide();
        }
    }
}
