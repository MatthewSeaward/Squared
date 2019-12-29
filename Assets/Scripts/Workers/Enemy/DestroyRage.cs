using Assets.Scripts.Workers.Enemy.Piece_Selection;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using UnityEngine;

namespace Assets.Scripts.Workers.Enemy
{
    public class DestroyRage : PieceSelectionRage
    {
        protected override PieceSelectionValidator pieceSelectionValidator { get; set; } = new StandardSelectionPieceValidator();
        protected override IPieceSelection pieceSelection { get; set; } = new RandomPieceSelector();

        protected override void InvokeRageActionOnPiece(ISquarePiece piece)
        {
            piece.DestroyPiece();
        }
    }
}
