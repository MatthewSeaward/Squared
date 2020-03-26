using System.Collections.Generic;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.Workers.Piece_Effects.Piece_Connection
{
    public class TwoSpriteConnection : IPieceConnection, ILayeredSprite
    {
        public Colour SecondColour { get; set; }
        public List<GameObject> Layers { get ; set; }

        private ISquarePiece _squarePiece;        

        public TwoSpriteConnection(ISquarePiece squarePiece,  Colour pieceColour)
        {
            SecondColour = pieceColour;
            _squarePiece = squarePiece;
        }

        public bool ConnectionValidTo(ISquarePiece nextPiece)
        {
            if (!ConnectionHelper.AdjancentToLastPiece(_squarePiece, nextPiece))
            {
                return false;
            }

            if (nextPiece != null  && nextPiece.PieceConnection is AnyAdjancentConnection)
            {
                return true;
            }

            if (nextPiece != null && nextPiece.PieceConnection is TwoSpriteConnection)
            {
                var fade = nextPiece.PieceConnection as TwoSpriteConnection;
                if (fade.SecondColour == _squarePiece.PieceColour || fade.SecondColour == SecondColour)
                {
                    return true;
                }
            }

            if (nextPiece != null && nextPiece.PieceColour != _squarePiece.PieceColour && nextPiece.PieceColour != SecondColour)
            {
                return false;
            }         

            return true;
        }

        public Sprite[] GetSprites()
        {
            return new Sprite[]
            {
                GameResources.Sprites["Fade" + (int) SecondColour]
            };
        }
    }
}
