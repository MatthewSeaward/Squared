using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.Helpers;
using NUnit.Framework;

namespace Assets.Scripts.Editor.Workers.Events.Events.Validators
{
    [Category("Events")]
    public class SpecificPieceSelectionValidatorTests
    {
        [Test]
        public void CorrectType()
        {
            var sut = new SpecificPieceSelectionValidator() { specificPiece = PieceBuilderDirector.PieceTypes.Rainbow };

            var piece = TestHelpers.CreatePiece();
            piece.Type = PieceBuilderDirector.PieceTypes.Rainbow;

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsTrue(validForSelection);
        }

        [Test]
        public void IncorrectType()
        {
            var sut = new SpecificPieceSelectionValidator() { specificPiece = PieceBuilderDirector.PieceTypes.Rainbow };

            var piece = TestHelpers.CreatePiece();
            piece.Type = PieceBuilderDirector.PieceTypes.Swapping;

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsFalse(validForSelection);
        }
    }
}
