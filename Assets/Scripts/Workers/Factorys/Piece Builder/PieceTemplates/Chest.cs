using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Piece_Effects.Destruction;
using Assets.Scripts.Workers.Piece_Effects.On_Destroy;
using Assets.Scripts.Workers.Piece_Effects.Piece_Connection;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.Workers.Factorys.PieceTemplates
{
    class Chest : PieceBuilder
    {
        protected override void BuildBehaviours()
        {
            squarePiece.PieceBehaviour = null;
        }

        protected override void BuildConnection()
        {
            squarePiece.PieceConnection = new NoConnection();
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
            squarePiece.OnDestroy = new BonusPoints(squarePiece);
        }

        protected override void BuildScoring()
        {
            squarePiece.Scoring = new SingleScore(scoreValue);
        }

        protected override (Sprite sprite, Colour colour) GetSprite()
        {
            return (GameResources.Sprites["Chest"], Colour.None);
        }
    }
}
