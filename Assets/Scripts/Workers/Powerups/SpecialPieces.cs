using Assets.Scripts.Workers.Enemy;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class SpecialPieces : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Rainbow Powerup"];
        
        public string Name => "Rainbow Rain";

        public string Description => "Randomly replaces some pieces with Rainbows.";

        public bool Enabled => true;

        private ChangeRandomPiece ChangePiece = new ChangeRandomPiece() { NewPieceType = PieceFactory.PieceTypes.Rainbow, SelectionAmount = 3 };

        public void Invoke()
        {
            ChangePiece.InvokeRage();
        }

        public void Update(float deltaTime)
        {
            ChangePiece.Update(deltaTime);
        }
    }
}
