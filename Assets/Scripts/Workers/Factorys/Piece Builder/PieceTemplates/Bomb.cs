using Assets.Scripts.Workers.Factorys.Helpers;
using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Piece_Effects.Destruction;
using Assets.Scripts.Workers.Piece_Effects.On_Destroy;
using Assets.Scripts.Workers.Piece_Effects.Piece_Connection;
using UnityEngine;

namespace Assets.Scripts.Workers.Factorys.PieceTemplates
{
    class Bomb : PieceBuilder
    {
        protected override void BuildBehaviours()
        {
            squarePiece.PieceBehaviour = null;
        }

        protected override void BuildConnection()
        {
            squarePiece.PieceConnection = new StandardConnection(squarePiece);
        }

        protected override void BuildDestroyEffect()
        {
            squarePiece.DestroyPieceHandler = new DestroyTriggerFall(squarePiece);
        }

        protected override void BuildOnCollection()
        {
            squarePiece.OnCollection = null;
        }

        protected override void BuildOnDestroy()
        {
            squarePiece.OnDestroy = new Explosion(squarePiece);
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
