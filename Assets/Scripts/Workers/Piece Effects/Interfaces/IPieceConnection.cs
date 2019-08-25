namespace Assets.Scripts.Workers.Piece_Effects.Interfaces
{
    public interface IPieceConnection
    {
        bool ConnectionValid(ISquarePiece selectedPiece);
        bool ConnectionValid(ISquarePiece selectedPiece, ISquarePiece nextPiece);
    }
}
