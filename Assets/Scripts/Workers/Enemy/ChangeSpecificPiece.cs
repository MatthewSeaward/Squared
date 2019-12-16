using System;
using Assets.Scripts.Workers.Enemy.Piece_Selection;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Helpers;
using static PieceBuilderDirector;

namespace Assets.Scripts.Workers.Enemy
{
    class ChangeSpecificPiece : PieceSelectionRage
    {
        protected override PieceSelectionValidator pieceSelectionValidator { get; set; } = new SpecificPieceSelecitonValidator();
        protected override IPieceSelection pieceSelection { get; set; } = new SpecificPieceSelection();

        public PieceTypes To { get; }

        public ChangeSpecificPiece(PieceTypes from, PieceTypes to)
        {
            this.To = to;

            (pieceSelectionValidator as SpecificPieceSelecitonValidator).specificPiece = from;
        }

      
        protected override void InvokeRageAction(ISquarePiece piece)
        {
            SquarePieceHelpers.ChangePiece(piece, To);
        }
    }
}
