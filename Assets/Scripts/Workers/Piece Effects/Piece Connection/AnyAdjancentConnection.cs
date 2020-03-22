using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;

namespace Assets.Scripts.Workers.Piece_Effects.Piece_Connection
{
    public class AnyAdjancentConnection : IPieceConnection
    {
        public bool ConnectionValid(ISquarePiece selectedPiece)
        {
            return ConnectionValid(selectedPiece, PieceSelectionManager.Instance.LastPiece);
        }

        public bool ConnectionValid(ISquarePiece selectedPiece, ISquarePiece nextPiece)
        {
            if (!ConnectionHelper.AdjancentToLastPiece(selectedPiece, nextPiece))
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
