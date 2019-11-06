namespace Assets.Scripts.Workers.UserPieceSelection
{
    public interface IUserPieceSelection
    {
        void Piece_MouseDown(ISquarePiece piece, bool checkForAdditional);
        void Piece_MouseUp(ISquarePiece piece);
        void Piece_MouseEnter(ISquarePiece piece);
    }
}