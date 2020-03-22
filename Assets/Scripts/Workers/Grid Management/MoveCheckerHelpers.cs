using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;

namespace Assets.Scripts.Workers.Grid_Management
{
    public static class MoveCheckerHelpers
    {
        public static bool CheckSpot(ISquarePiece piece, int x, int y)
        {
            var nextPiece = PieceManager.Instance.GetPiece(x, y);
            if (nextPiece == null)
            {
                return false;
            }

            return ConnectionHelper.ValidConnectionBetween(piece, nextPiece);
        }
    }
}
