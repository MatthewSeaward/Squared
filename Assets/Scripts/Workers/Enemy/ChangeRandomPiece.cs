using Assets.Scripts.Workers.Enemy.Piece_Selection;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Helpers;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;

namespace Assets.Scripts.Workers.Enemy
{
    public class ChangeRandomPiece : PieceSelectionRage
    {
        public PieceTypes NewPieceType = PieceTypes.Empty;

        protected override PieceSelectionValidator pieceSelectionValidator { get; set; } = new StandardSelectionPieceValidator();
        protected override IPieceSelection pieceSelection { get; set; } = new RandomPieceSelector();

        protected override void InvokeRageActionOnPiece(ISquarePiece piece)
        {
            SquarePieceHelpers.ChangePiece(piece, NewPieceType);
        }
    }
}
