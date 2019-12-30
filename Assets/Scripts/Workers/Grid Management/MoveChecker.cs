using Assets.Scripts.Workers.Managers;

namespace Assets.Scripts.Workers.Grid_Management
{
    public static class MoveChecker
    {
        public static bool AvailableMove()
        {
            for (int x = 0; x < PieceManager.Instance.NumberOfColumns; x++)
            {
                for (int y = 0; y < PieceManager.Instance.NumberOfRows; y++)
                {
                    var piece = PieceManager.Instance.GetPiece(x, y);
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
