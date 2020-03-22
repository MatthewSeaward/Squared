namespace Assets.Scripts.Workers.Piece_Effects.Interfaces
{
    public interface IPieceConnection
    {
        bool ConnectionValidTo(ISquarePiece nextPiece);
    }
}
