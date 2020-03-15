using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Piece_Effects.Destruction;
using Assets.Scripts.Workers.Test_Mockers.Lerp;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Scripts.Editor.Workers.Events.Events.Validators
{
    [Category("Events")]
    class StandardPieceSelectionValdiatorTests
    {
        [Test]
        public void NullPiece()
        {
            var sut = new StandardSelectionPieceValidator();

            var validForSelection = sut.ValidForSelection(null);

            Assert.IsFalse(validForSelection);
        }

        [Test]
        public void InactivePiece()
        {
            var sut = new StandardSelectionPieceValidator();

            var piece = TestHelpers.CreatePiece(0, 0, false);

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsFalse(validForSelection);
        }

        [Test]
        public void LerpInProgress()
        {
            var sut = new StandardSelectionPieceValidator();

            var go = new GameObject();
            var piece = go.AddComponent<SquarePiece>();
            go.AddComponent<LerpInProgress>();

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsFalse(validForSelection);
        }

        [Test]
        public void ToBeDestroyed()
        {
            var sut = new StandardSelectionPieceValidator();

            var go = new GameObject();
            var piece = go.AddComponent<SquarePiece>();
            piece.DestroyPieceHandler = new DestroyTriggerFall(piece);
            piece.DestroyPieceHandler.NotifyOfDestroy();

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsFalse(validForSelection);
        }

        [Test]
        public void ChestPiece()
        {
            var sut = new StandardSelectionPieceValidator();

            var piece = TestHelpers.CreatePiece(0, 0);
            piece.Type = PieceBuilderDirector.PieceTypes.Chest;

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsFalse(validForSelection);
        }

        [Test]
        public void LockedSwap()
        {
            var sut = new StandardSelectionPieceValidator();

            var piece = TestHelpers.CreatePiece(0, 0);
            piece.DestroyPieceHandler = new LockedSwap(piece);

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsFalse(validForSelection);
        }

        [Test]
        public void ValidSelection()
        {
            var sut = new StandardSelectionPieceValidator();

            var go = new GameObject();
            var piece = go.AddComponent<SquarePiece>();
            go.AddComponent<LerpNotInProgress>();

            var validForSelection = sut.ValidForSelection(piece);

            Assert.IsTrue(validForSelection);
        }
    }
}