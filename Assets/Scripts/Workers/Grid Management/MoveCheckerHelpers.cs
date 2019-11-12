namespace Assets.Scripts.Workers.Grid_Management
{
    public static class MoveCheckerHelpers
    {
        public static bool CheckSpot(ISquarePiece piece, int x, int y)
        {
            var nextPiece = PieceController.GetPiece(x, y);
            if (nextPiece == null)
            {
                return false;
            }

            return piece.PieceConnection.ConnectionValid(piece, nextPiece);
        }
    }
}
