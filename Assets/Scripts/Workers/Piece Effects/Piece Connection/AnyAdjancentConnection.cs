using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;

namespace Assets.Scripts.Workers.Piece_Effects.Piece_Connection
{
    public class AnyAdjancentConnection : IPieceConnection
    {
        private ISquarePiece _squarePiece;

        public AnyAdjancentConnection(ISquarePiece squarePiece)
        {
            _squarePiece = squarePiece;
        }


        public bool ConnectionValidTo(ISquarePiece nextPiece)
        {
            if (!ConnectionHelper.AdjancentToLastPiece(_squarePiece, nextPiece))
            {
                return false;
            }

            if (nextPiece.PieceConnection is NoConnection)
            {
                return false;
            }

            return true;
        }
    }
}
