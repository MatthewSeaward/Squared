using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.Workers.Piece_Effects.Piece_Connection
{
    public class TwoSpriteConnection : IPieceConnection, ILayeredSprite
    {
        public Colour BannedPiece { get; private set; }

        public TwoSpriteConnection(Colour pieceColour)
        {
            BannedPiece = pieceColour;
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

            if (nextPiece != null && nextPiece.Sprite != selectedPiece.Sprite && nextPiece.PieceColour != BannedPiece)
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
                GameResources.Sprites["Fade" + (int) BannedPiece]
            };
        }
    }
}
