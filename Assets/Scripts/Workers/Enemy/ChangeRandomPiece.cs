using Assets.Scripts.Workers.Enemy.Piece_Selection;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Helpers;
using static PieceBuilderDirector;

namespace Assets.Scripts.Workers.Enemy
{
    public class ChangeRandomPiece : PieceSelectionRage
    {
        public PieceTypes NewPieceType = PieceTypes.Empty;

        protected override PieceSelectionValidator pieceSelectionValidator { get; set; } = new StandardSelectionPieceValidator();
        protected override IPieceSelection pieceSelection { get; set; } = new RandomPieceSelector();

        protected override void InvokeRageAction(ISquarePiece piece)
        {
            SquarePieceHelpers.ChangePiece(piece, NewPieceType);
        }
    }
}
