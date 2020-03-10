using Assets.Scripts.Workers.Factorys.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Piece_Effects.Destruction;
using Assets.Scripts.Workers.Piece_Effects.Piece_Connection;
using UnityEngine;

namespace Assets.Scripts.Workers.Factorys.PieceTemplates
{
    class FadePiece : PieceBuilder
    {
        private ISquarePiece _piece;

        public FadePiece(ISquarePiece piece)
        {
            _piece = piece;
        }

        protected override void BuildBehaviours()
        {
            squarePiece.PieceBehaviour = null;
        }

        protected override void BuildConnection()
        {
            var secondPiece = GetSprite();
            while (secondPiece.sprite == _piece.Sprite)
            {
                secondPiece = GetSprite();
            }

            squarePiece.PieceConnection = new TwoSpriteConnection(secondPiece.colour);
        }

        protected override void BuildDestroyEffect()
        {
            if (initialsetup)
            {
                squarePiece.DestroyPieceHandler = new LockedSwap(squarePiece);
            }
            else
            {
                squarePiece.DestroyPieceHandler = new DestroyTriggerFall(squarePiece);
            }
        }

        protected override void BuildOnCollection()
        {
            squarePiece.OnCollection = null;
        }

        protected override void BuildOnDestroy()
        {
            squarePiece.OnDestroy = null;
        }

        protected override void BuildScoring()
        {
            squarePiece.Scoring = new SingleScore(scoreValue);
        }

        protected override (Sprite sprite, SquarePiece.Colour colour) GetSprite()
        {
            return PieceCreationHelpers.GetRandomSprite();
        }
    }
}
