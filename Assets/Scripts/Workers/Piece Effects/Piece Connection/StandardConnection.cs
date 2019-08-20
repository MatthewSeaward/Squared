using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;

namespace Assets.Scripts.Workers.Piece_Effects
{
    public class StandardConnection : IPieceConnection
    {
        public bool ConnectionValid(ISquarePiece selectedPiece)
        {
            if (!ConnectionHelper.AdjancentToLastPiece(selectedPiece))
            {
                return false;
            }

            if (PieceSelectionManager.Instance.LastPiece != null  && PieceSelectionManager.Instance.LastPiece.PieceConnection is AnyAdjancentConnection)
            {
                return true;
            }

            if (PieceSelectionManager.Instance.LastPiece != null && PieceSelectionManager.Instance.LastPiece.Sprite != selectedPiece.Sprite)
            {
                return false;
            }
       

            return true;
        }
    }
}
