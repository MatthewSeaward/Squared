using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using System;
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

        public List<ISquarePiece> SelectPieces(PieceSelectionValidator validator, int total)
        {
            return PieceController.Pieces.Where(x => Positions.Contains(x.Position)).ToList();
        }

    }
}
