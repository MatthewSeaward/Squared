using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.Piece_Connection
{
    public class StandardConnection : IPieceConnection
    {
        public ISquarePiece _squarePiece;

        public List<GameObject> Layers { get; set; }

        public StandardConnection(ISquarePiece squarePiece)
        {
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

            if (nextPiece != null && nextPiece.PieceColour != _squarePiece.PieceColour)
            {
                return false;
            }

            return true;
        }
    }
}
