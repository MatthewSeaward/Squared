using Assets.Scripts.Workers.Enemy;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class ExtraLife : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Extra Life"];

        public string Name => "Extra Life";

        public string Description => "Sends down some extra life pieces that you can try and collect to restore some lives.";

        public bool Enabled => true;

        private ChangeRandomPiece ChangePiece = new ChangeRandomPiece() { NewPieceType = PieceBuilderDirector.PieceTypes.Heart, SelectionAmount = 2 };

        public void Invoke()
        {
            ChangePiece.InvokeRage();
        }

        public void Update(float deltaTime)
        {
            ChangePiece.Update(deltaTime);
        }

        public void MoveCompleted()
        {
        }
    }
}
