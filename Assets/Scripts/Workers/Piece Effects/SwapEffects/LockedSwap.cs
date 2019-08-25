using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;
using Assets.Scripts.Workers.IO.Data_Entities;

namespace Assets.Scripts.Workers.Piece_Effects.SwapEffects
{
    public class LockedSwap : ISwapEffect, ILayeredSprite
    {
        private static Sprite _sprite;

        public Sprite ProcessSwap(ISquarePiece currentPiece)
        {
            return currentPiece.Sprite; 
        }

        public Sprite GetSprite()
        { 
            return GameResources.Sprites["Padlock"];
        }
    }
}
