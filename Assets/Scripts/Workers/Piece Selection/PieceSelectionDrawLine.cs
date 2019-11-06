using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.UserPieceSelection;
using System;

namespace Assets.Scripts.Workers.Piece_Selection
{
    public class PieceSelectionDrawLine : IUserPieceSelection
    {
        public void Piece_MouseDown(ISquarePiece piece, bool checkForAdditional)
        {
            if (PieceSelectionManager.Instance.AlreadySelected(piece))
            {
                return;
            }

            if (!piece.PieceConnection.ConnectionValid(piece))
            {
                return;
            }

            if (piece.DestroyPieceHandler.ToBeDestroyed)
            {
                return;
            }

            PieceSelectionManager.Instance.Add(piece, checkForAdditional);

            piece.DestroyPieceHandler.OnPressed();
            piece.SetMouseDown(true);
        }

        public void Piece_MouseEnter(ISquarePiece piece)
        {
            if (ConnectionHelper.AdjancentToLastPiece(piece) && PieceSelectionManager.Instance.PieceCanBeRemoved(piece))
            {
                PieceSelectionManager.Instance.RemovePiece();
            }
            else
            {
                piece.Pressed(true);
            }
        }

        public void Piece_MouseUp(ISquarePiece piece)
        {
        }
    }
}
