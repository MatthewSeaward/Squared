using Assets.Scripts.Workers.Piece_Effects.Interfaces;

namespace Assets.Scripts.Workers.Piece_Effects.Piece_Connection
{
    class NoConnection : IPieceConnection
    {
        public bool ConnectionValid(ISquarePiece selectedPiece)
        {
            return false;
        }

        public bool ConnectionValid(ISquarePiece selectedPiece, ISquarePiece nextPiece)
        {
            return false;
        }
    }
}
