using Assets.Scripts.UI;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class DoubleMove : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Double Move"];

        public string Name => "Double Move";

        public string Description => "Lets you take two moves at once before pieces fall.";

        public bool Enabled => true;

        public void Invoke()
        {
            PieceSelectionManager.Instance.ChangeMovesAllowedPerTurn(2);
            ToastPanel.Instance.ShowText("Make two moves");
        }

        public void MoveCompleted()
        {
            ToastPanel.Instance.Hide();
        }     
    }
}
