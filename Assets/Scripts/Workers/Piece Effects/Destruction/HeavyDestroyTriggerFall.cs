using System;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using Assets.Scripts.Workers.Piece_Effects.SwapEffects;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.Destruction
{
    internal class HeavyDestroyTriggerFall : DestroyTriggerFall, ILayeredSprite
    {
        private bool pieceDestroyed = false;

        public HeavyDestroyTriggerFall(SquarePiece squarePiece) : base(squarePiece)
        {
        }

        public override void FallTo(int y)
        {
            base.FallTo(y);
            if (pieceDestroyed)
            {
                pieceDestroyed = false;
                return;
            }

            var pieceBelow = PieceController.GetPiece(_squarePiece.Position.x, _squarePiece.Position.y + 1);

            if (pieceBelow == null || !PieceCanBeDestroyed(pieceBelow))
            { 
                return;
            }

            pieceBelow.DestroyPiece();
            pieceDestroyed = true;
        }

        private bool PieceCanBeDestroyed(SquarePiece pieceBelow)
        {
            if (pieceBelow.DestroyPieceHandler is HeavyDestroyTriggerFall)
            {
                return false;
            }

            if (pieceBelow.SwapEffect is LockedSwap)
            {
                return false;
            }

            return true; 
        }

        public override void Reset()
        {
            base.Reset();
            pieceDestroyed = false;
        }

        public Sprite GetSprite()
        {
            return GameResources.Sprites["Rock"];
        }
    }
}
