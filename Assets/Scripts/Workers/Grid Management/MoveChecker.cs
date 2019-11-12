namespace Assets.Scripts.Workers.Grid_Management
{
    public static class MoveChecker
    {
        public static bool AvailableMove()
        {
            for (int x = 0; x < PieceController.NumberOfColumns; x++)
            {
                for (int y = 0; y < PieceController.NumberOfRows; y++)
                {
                    var piece = PieceController.GetPiece(x, y);
                    if (piece == null )
                    {
                        continue;
                    }

                    if (MoveCheckerHelpers.CheckSpot(piece, x + 1, y + 1) ||
                        MoveCheckerHelpers.CheckSpot(piece, x + 1, y) ||
                        MoveCheckerHelpers.CheckSpot(piece, x + 1, y - 1) ||
                        MoveCheckerHelpers.CheckSpot(piece, x , y - 1))
                    {
                        return true;
                    }

                }
            }

            return false;
        }   
    }
}
