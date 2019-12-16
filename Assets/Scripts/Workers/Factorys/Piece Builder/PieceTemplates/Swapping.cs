using Assets.Scripts.Workers.Factorys.Piece_Builder.Helpers;
using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Piece_Effects.Destruction;
using UnityEngine;

namespace Assets.Scripts.Workers.Factorys.Piece_Builder.PieceTemplates
{
    class Swapping : PieceBuilder
    {
        protected override void BuildBehaviours()
        {
            squarePiece.PieceBehaviour = new SwapSpriteBehaviour();
        }

        protected override void BuildConnection()
        {
            squarePiece.PieceConnection = new StandardConnection();
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
