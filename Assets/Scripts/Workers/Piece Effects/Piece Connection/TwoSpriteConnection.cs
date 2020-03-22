using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.Workers.Piece_Effects.Piece_Connection
{
    public class TwoSpriteConnection : IPieceConnection, ILayeredSprite
    {
        public Colour SecondColour { get; private set; }

        public TwoSpriteConnection(Colour pieceColour)
        {
            SecondColour = pieceColour;
        }

        public bool ConnectionValid(ISquarePiece selectedPiece, ISquarePiece nextPiece)
        {
            if (!ConnectionHelper.AdjancentToLastPiece(selectedPiece, nextPiece))
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
                if (fade.SecondColour == selectedPiece.PieceColour || fade.SecondColour == SecondColour)
                {
                    return true;
                }
            }

            if (nextPiece != null && nextPiece.Sprite != selectedPiece.Sprite && nextPiece.PieceColour != SecondColour)
            {
                return false;
            }

         

            return true;
        }

        public bool ConnectionValid(ISquarePiece selectedPiece)
        {
            return ConnectionValid(selectedPiece, PieceSelectionManager.Instance.LastPiece);
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
