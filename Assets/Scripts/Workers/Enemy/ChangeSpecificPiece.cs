using Assets.Scripts.Workers.Enemy.Piece_Selection;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Helpers;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;

namespace Assets.Scripts.Workers.Enemy
{
    public class ChangeSpecificPiece : PieceSelectionRage
    {
        protected override PieceSelectionValidator pieceSelectionValidator { get; set; } = new SpecificPieceSelectionValidator();
        protected override IPieceSelection pieceSelection { get; set; } = new SpecificPieceSelection(false);

        public PieceTypes To { get; }
        public PieceTypes From { get; }

        public ChangeSpecificPiece(PieceTypes from, PieceTypes to)
        {
            this.To = to;
            this.From = from;

            (pieceSelectionValidator as SpecificPieceSelectionValidator).specificPiece = from;
        }
      
        protected override void InvokeRageActionOnPiece(ISquarePiece piece)
        {
            SquarePieceHelpers.ChangePiece(piece, To);
        }
    }
}