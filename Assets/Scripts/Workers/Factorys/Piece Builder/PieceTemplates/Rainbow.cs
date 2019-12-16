using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Piece_Effects.Destruction;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.Workers.Factorys.Piece_Builder.PieceTemplates
{
    class Rainbow : PieceBuilder
    {
        protected override void BuildBehaviours()
        {
            squarePiece.PieceBehaviour = null;
        }

        protected override void BuildConnection()
        {
            squarePiece.PieceConnection = new AnyAdjancentConnection();
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
            return (GameResources.Sprites["Rainbow"], Colour.None);
        }
    }
}
