using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Piece_Effects.Piece_Connection;
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
        public void NoColourSet()
        {
            var sut = new ColourSelectionValidator() { specificColour = SquarePiece.Colour.None };

            var piece = TestHelpers.CreatePiece();
            piece.PieceColour = SquarePiece.Colour.Red;

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsTrue(validForSelection);
        }
        
        [Test]
        public void IgnorePiecesWithoutColour()
        {
            var sut = new ColourSelectionValidator() { specificColour = SquarePiece.Colour.None };

            var piece = TestHelpers.CreatePiece();
            piece.PieceColour = SquarePiece.Colour.None;

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsFalse(validForSelection);
        }

        [Test]
        public void FadeColours_Fade1()
        {
            var sut = new ColourSelectionValidator() { specificColour = SquarePiece.Colour.DarkBlue };

            var piece = TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.FadePiece);
            piece.PieceColour = SquarePiece.Colour.DarkBlue;
            piece.PieceConnection = new TwoSpriteConnection(piece, SquarePiece.Colour.Red);

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsTrue(validForSelection);
        }

        [Test]
        public void FadeColours_Fade2()
        {
            var sut = new ColourSelectionValidator() { specificColour = SquarePiece.Colour.DarkBlue };

            var piece = TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.FadePiece);
            piece.PieceColour = SquarePiece.Colour.Red;
            piece.PieceConnection = new TwoSpriteConnection(piece, SquarePiece.Colour.DarkBlue);

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsTrue(validForSelection);
        }
    }
}
