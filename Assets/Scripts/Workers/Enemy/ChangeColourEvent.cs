using Assets.Scripts.Workers.Enemy.Piece_Selection;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Helpers;
using static SquarePiece;

namespace Assets.Scripts.Workers.Enemy
{
    public class ChangeColourEvent : PieceSelectionRage
    {
        protected override PieceSelectionValidator pieceSelectionValidator { get; set; } = new ColourSelectionValidator();
        protected override IPieceSelection pieceSelection { get; set; } = new SpecificPieceSelection(true);

        public Colour To { get; }
        public Colour From { get; }

        public ChangeColourEvent(Colour from, Colour to)
        {
            this.To = to;
            this.From = from;

            (pieceSelectionValidator as ColourSelectionValidator).specificColour = From;
        }

        protected override void InvokeRageActionOnPiece(ISquarePiece piece)
        {
            SquarePieceHelpers.ChangePieceColour(piece, To);
        }
    }
}
