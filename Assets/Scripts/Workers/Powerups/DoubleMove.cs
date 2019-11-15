using Assets.Scripts.UI;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class DoubleMove : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Rune"];

        public string Name => "Double Move";

        public string Description => "Let's you take two moves at once before pieces fall.";

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

        public void Update(float deltaTime)
        {
        }
    }
}
