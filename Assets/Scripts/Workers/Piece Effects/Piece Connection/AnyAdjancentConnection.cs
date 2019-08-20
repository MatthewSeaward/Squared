using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;

namespace Assets.Scripts.Workers.Piece_Effects
{
    public class AnyAdjancentConnection : IPieceConnection
    {
        public bool ConnectionValid(ISquarePiece selectedPiece)
        {
            if (!ConnectionHelper.AdjancentToLastPiece(selectedPiece))
            {
                return false;
            }

            return true;
        }
    }
}
