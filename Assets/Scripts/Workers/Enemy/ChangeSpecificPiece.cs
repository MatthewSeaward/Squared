using Assets.Scripts.Workers.Enemy.Piece_Selection;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Helpers;
using static PieceBuilderDirector;

namespace Assets.Scripts.Workers.Enemy
{
    public class ChangeSpecificPiece : PieceSelectionRage
    {
        protected override PieceSelectionValidator pieceSelectionValidator { get; set; } = new SpecificPieceSelecitonValidator();
        protected override IPieceSelection pieceSelection { get; set; } = new SpecificPieceSelection();

        public PieceTypes To { get; }
        public PieceTypes From { get; }

        public ChangeSpecificPiece(PieceTypes from, PieceTypes to)
        {
            this.To = to;
            this.From = from;

            (pieceSelectionValidator as SpecificPieceSelecitonValidator).specificPiece = from;
        }
      
        protected override void InvokeRageActionOnPiece(ISquarePiece piece)
        {
            SquarePieceHelpers.ChangePiece(piece, To);
        }
    }
}