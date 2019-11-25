using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;
using Assets.Scripts.Workers.IO.Data_Entities;

namespace Assets.Scripts.Workers.Piece_Effects.Destruction
{
    public class LockedSwap : IPieceDestroy, ILayeredSprite
    {
        private static Sprite _sprite;
        private ISquarePiece piece;

        public bool ToBeDestroyed => false;
         
        public LockedSwap(ISquarePiece piece)
        {
            this.piece = piece;
        }

        public Sprite GetSprite()
        { 
            return GameResources.Sprites["Padlock"];
        }

        public void OnPressed()
        {
        }

        public void Update()
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
