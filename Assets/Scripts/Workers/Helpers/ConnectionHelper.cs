namespace Assets.Scripts.Workers.Helpers
{
    public static class ConnectionHelper
    {
        public static bool ValidConnectionBetween(ISquarePiece first, ISquarePiece second)
        {
            return first.PieceConnection.ConnectionValid(first, second) || second.PieceConnection.ConnectionValid(second, first);
        }

        public static bool AdjancentToLastPiece(ISquarePiece selectedPiece)
        {
            return AdjancentToLastPiece(selectedPiece, PieceSelectionManager.Instance.LastPiece);
        }

        public static bool AdjancentToLastPiece(ISquarePiece selectedPiece, ISquarePiece lastPiece)
        {
            if (lastPiece == null)
            {
                return true;
            }

            var myx = selectedPiece.Position.x;
            var myy = selectedPiece.Position.y;

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

        public static bool DiagonalConnection(ISquarePiece firstPiece, ISquarePiece secondPiece)
        {
            return firstPiece.Position.x != secondPiece.Position.x &&
                   firstPiece.Position.y != secondPiece.Position.y;
        }
    }
}
