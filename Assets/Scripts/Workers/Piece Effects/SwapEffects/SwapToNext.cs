using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.SwapEffects
{
    class SwapToNext : ISwapEffect
    {
        public Sprite ProcessSwap(ISquarePiece currentPiece)
        {
            return PieceFactory.Instance.CreateRandomSprite();
        }
    }
}
