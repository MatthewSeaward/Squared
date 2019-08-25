using System;

namespace Assets.Scripts.Workers
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

                    if (CheckSpot(piece, x + 1, y + 1) ||
                        CheckSpot(piece, x + 1, y) ||
                        CheckSpot(piece, x + 1, y - 1) ||
                        CheckSpot(piece, x , y - 1))
                    {
                        return true;
                    }

                }
            }

            return false;
        }

        private static bool CheckSpot(ISquarePiece piece, int x, int y)
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
