using UnityEngine;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection_Validator
{
    class PositionValidator : PieceSelectionValidator
    {
        public Vector2Int position; 

        public override bool ValidForSelection(ISquarePiece piece)
        {
            return piece.Position == position;
        }
    }
}
