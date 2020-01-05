using Assets.Scripts.Managers;
using Assets.Scripts.Workers.Grid_Management;
using Assets.Scripts.Workers.Managers;
using System.Linq;

namespace Assets.Scripts.Workers.UserPieceSelection
{
    public class PieceSelectionModeDrawLine : IPieceSelectionMode
    {
        private static IPathFinder pathFinder = new AStarPathFinder(); 

        public void Piece_MouseDown(ISquarePiece piece, bool checkForAdditional)
        {
            if (GameManager.Instance.GamePaused || MenuProvider.Instance.OnDisplay)
            {
                return;
            }
            
            if (PieceSelectionManager.Instance.AlreadySelected(piece))
            {
                return;
            }

            if (piece.DestroyPieceHandler.ToBeDestroyed)
            {
                return;
            }

            if (piece.PieceConnection.ConnectionValid(piece))
            {
                PieceSelectionManager.Instance.Add(piece, checkForAdditional);
                piece.Selected();
            }
            else
            {
                TryToMapPath(piece, checkForAdditional);
            }
        }

        public void Piece_MouseEnter(ISquarePiece piece)
        {
            if (PieceSelectionManager.Instance.PieceCanBeRemoved(piece))
            {
                PieceSelectionManager.Instance.RemovePiece(piece);
            }
            else
            {
                piece.Pressed(true);
            }
        }

        public void Piece_MouseUp(ISquarePiece piece)
        {
        }

        private static void TryToMapPath(ISquarePiece piece, bool checkForAdditional)
        {
            var path = pathFinder.FindPath(PieceSelectionManager.Instance.LastPiece.Position, piece.Position, GameManager.Instance.Restriction);
            if (path.Count == 0)
            {
                return;
            }

            var pieces = path.Select(point => PieceManager.Instance.GetPiece(point.x, point.y));

            // If any of these pieces are currently selected then abort. Things can get a little weird otherwise.
            var alreadySelected = false;
            foreach (var p in pieces)
            {
                if (p == PieceSelectionManager.Instance.LastPiece)
                {
                    continue;
                }
                if (PieceSelectionManager.Instance.CurrentPieces.Contains(p))
                {
                    alreadySelected = true;
                    break;
                }
            }

            if (alreadySelected)
            {
                return;
            }

            // All good so select all of these pieces.
            foreach (var p in pieces)
            {
                PieceSelectionManager.Instance.Add(p, checkForAdditional);
                p.Selected();
            }
        }
    }    
}
