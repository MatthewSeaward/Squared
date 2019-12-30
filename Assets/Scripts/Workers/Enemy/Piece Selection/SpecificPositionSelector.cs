using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection
{
    public class SpecificPositionSelector : IPieceSelection
    {
        List<Vector2Int> Positions;

        public SpecificPositionSelector(List<Vector2Int> positions)
        {
            this.Positions = positions;
        }

        public bool CanBeUsed(PieceSelectionValidator validator, int total)
        {
            return PieceManager.Instance.Pieces.Any(x => Positions.Contains(x.Position));
        }

        public List<ISquarePiece> SelectPieces(PieceSelectionValidator validator, int total)
        {
            return PieceManager.Instance.Pieces.Where(x => Positions.Contains(x.Position)).ToList();
        }
    }
}
