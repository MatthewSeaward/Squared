using Assets.Scripts.Workers.Piece_Effects.Piece_Connection;
using static SquarePiece;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection_Validator
{
    public class ColourSelectionValidator : PieceSelectionValidator
    {
        public Colour specificColour;

        public override bool ValidForSelection(ISquarePiece piece)
        {
            if (piece.PieceConnection is TwoSpriteConnection connection && connection.SecondColour == specificColour)
            {
                return true;
            }
            else
            {
                return specificColour == Colour.None || piece.PieceColour == specificColour;
            }
        }
    }
}
