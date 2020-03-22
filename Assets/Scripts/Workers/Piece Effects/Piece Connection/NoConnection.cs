using Assets.Scripts.Workers.Piece_Effects.Interfaces;

namespace Assets.Scripts.Workers.Piece_Effects.Piece_Connection
{
    public class NoConnection : IPieceConnection
    {
        public bool ConnectionValidTo(ISquarePiece nextPiece)
        {
            return false;
        }
    }
}
