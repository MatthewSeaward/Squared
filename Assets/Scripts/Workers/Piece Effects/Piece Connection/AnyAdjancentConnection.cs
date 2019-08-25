using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;

namespace Assets.Scripts.Workers.Piece_Effects
{
    public class AnyAdjancentConnection : IPieceConnection
    {
        public bool ConnectionValid(ISquarePiece selectedPiece)
        {
            return ConnectionValid(selectedPiece, PieceSelectionManager.Instance.LastPiece);
        }

        public bool ConnectionValid(ISquarePiece selectedPiece, ISquarePiece nextPiece)
        {
            if (!ConnectionHelper.AdjancentToLastPiece(nextPiece, nextPiece))
            {
                return false;
            }

            return true;
        }
    }
}
