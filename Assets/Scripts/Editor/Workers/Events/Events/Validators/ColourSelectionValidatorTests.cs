using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Helpers;
using NUnit.Framework;

namespace Assets.Scripts.Editor.Workers.Events.Events.Validators
{
    [Category("Events")]
    class ColourSelectionValidatorTests
    {
        [Test]
        public void SameColours()
        {
            var sut = new ColourSelectionValidator() { specificColour = SquarePiece.Colour.DarkBlue };

            var piece = TestHelpers.CreatePiece();
            piece.PieceColour = SquarePiece.Colour.DarkBlue;

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsTrue(validForSelection);
        }

        [Test]
        public void DifferentColours()
        {
            var sut = new ColourSelectionValidator() { specificColour = SquarePiece.Colour.DarkBlue };

            var piece = TestHelpers.CreatePiece();
            piece.PieceColour = SquarePiece.Colour.Red;

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsFalse(validForSelection);
        }

        [Test]
        public void NoColourSets()
        {
            var sut = new ColourSelectionValidator() { specificColour = SquarePiece.Colour.None };

            var piece = TestHelpers.CreatePiece();
            piece.PieceColour = SquarePiece.Colour.Red;

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsTrue(validForSelection);
        }
    }
}
