namespace Assets.Scripts.Workers.Helpers
{
    public static class ConnectionHelper
    {
        public static bool AdjancentToLastPiece(ISquarePiece selectedPiece)
        {
            if (PieceSelectionManager.Instance.CurrentPieces.Count == 0)
            {
                return true;
            }

            var myx = selectedPiece.Position.x;
            var myy = selectedPiece.Position.y;

            var lastPiece = PieceSelectionManager.Instance.LastPiece;
            var lastx = lastPiece.Position.x;
            var lasty = lastPiece.Position.y;

            // check above and below
            int gap = 1;
            if (CheckAdjancent(myx, myy + gap, lastx, lasty) ||
                CheckAdjancent(myx + gap, myy + gap, lastx, lasty) ||
                CheckAdjancent(myx + gap, myy, lastx, lasty) ||
                CheckAdjancent(myx + gap, myy - gap, lastx, lasty) ||
                CheckAdjancent(myx, myy - gap, lastx, lasty) ||
                CheckAdjancent(myx - gap, myy - gap, lastx, lasty) ||
                CheckAdjancent(myx - gap, myy, lastx, lasty) ||
                CheckAdjancent(myx - gap, myy + gap, lastx, lasty))
            {
                return true;
            }

            return false;
        }

        private static bool CheckAdjancent(float myx, float myy, float lastx, float lasty)
        {
            return myx == lastx && myy == lasty;
        }
    }
}
