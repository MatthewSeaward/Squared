using System;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using Assets.Scripts.Workers.Piece_Effects.Destruction;
using UnityEngine;
using Assets.Scripts.Workers.Managers;

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

            var pieceBelow = PieceManager.Instance.GetPiece(_squarePiece.Position.x, _squarePiece.Position.y + 1);

            if (pieceBelow == null || !PieceCanBeDestroyed(pieceBelow))
            { 
                return;
            }

            pieceBelow.DestroyPiece();
            pieceDestroyed = true;
        }

        private bool PieceCanBeDestroyed(ISquarePiece pieceBelow)
        {
            if (pieceBelow.DestroyPieceHandler is HeavyDestroyTriggerFall)
            {
                return false;
            }

            if (pieceBelow.DestroyPieceHandler is LockedSwap)
            {
                return false;
            }

            if (pieceBelow.DestroyPieceHandler is DestroyTriggerFall && (pieceBelow.DestroyPieceHandler as DestroyTriggerFall).ToBeDestroyed)
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

        public Sprite[] GetSprites()
        {
            return new Sprite[] { GameResources.Sprites["Rock"] };
        }
    }
}
