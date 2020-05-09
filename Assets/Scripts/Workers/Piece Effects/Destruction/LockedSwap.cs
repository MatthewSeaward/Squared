using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;
using Assets.Scripts.Workers.IO.Data_Entities;

namespace Assets.Scripts.Workers.Piece_Effects.Destruction
{
    public class LockedSwap : IPieceDestroy, ILayeredSprite
    {
        private ISquarePiece piece;

        public bool ToBeDestroyed => false;
         
        public LockedSwap(ISquarePiece piece)
        {
            this.piece = piece;
        }

        public LayeredSpriteSettings GetLayeredSprites()
        { 
            return new LayeredSpriteSettings() { Sprites = new Sprite[] { GameResources.Sprites["Padlock"] }, OrderInLayer = 6 };
        }

        public void OnPressed()
        {
        }

        public void OnDestroy()
        {
            piece.Deselected();
        }

        public void NotifyOfDestroy()
        {
        }

        public void Reset()
        {
        }
    }
}
