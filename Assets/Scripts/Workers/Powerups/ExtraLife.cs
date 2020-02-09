using Assets.Scripts.Workers.Enemy;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.Generic_Interfaces;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class ExtraLife : IPowerup, IUpdateable
    {
        public Sprite Icon => GameResources.Sprites["New Credit Powerup"];

        public string Name => "New Credit";

        public string Description => "Sends down some extra credit pieces that you can try and collect to restore some credits.";

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
