using Assets.Scripts.Managers;
using Assets.Scripts.UI;
using Assets.Scripts.Workers.Grid_Management;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Managers;
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

            if (move.Result == SearchResult.TimeOut)
            {
                ToastPanel.Instance.ShowText("Search timed out", 2f);
            }
            else if (move.Result == SearchResult.NoMoves || move.Move == null || move.Move.Count == 0)
            {
                ToastPanel.Instance.ShowText("No Reults", 2f);
            }
            else
            {
                PieceSelectionManager.Instance.PreformMove(move.Move);
            }
        }

        public void MoveCompleted()
        {
            GameManager.Instance.ChangePauseState(this, false);
            ToastPanel.Instance.Hide();
        }
    }
}
