using static SquarePiece;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection_Validator
{
    public class ColourSelectionValidator : PieceSelectionValidator
    {
        public Colour specificColour;

        public override bool ValidForSelection(ISquarePiece piece)
        {
            return specificColour == Colour.None || piece.PieceColour == specificColour;
        }
    }
}
