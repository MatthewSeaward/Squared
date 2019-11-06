using Assets.Scripts.Workers.UserPieceSelection;

namespace Assets.Scripts.Workers.Piece_Selection
{
    class PieceSelectionModeDestroyTap : IPieceSelectionMode
    {
        public void Piece_MouseDown(ISquarePiece piece, bool checkForAdditional)
        {
        }

        public void Piece_MouseEnter(ISquarePiece piece)
        {
        }

        public void Piece_MouseUp(ISquarePiece piece)
        {
            piece.DestroyPiece();
        }
    }
}
