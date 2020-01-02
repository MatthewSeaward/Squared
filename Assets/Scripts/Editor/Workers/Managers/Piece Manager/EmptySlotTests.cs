using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Editor.Workers.Managers.Piece_Manager
{
    [Category("Manager")]
    class EmptySlotTests
    {
        [SetUp]
        public void Setup()
        {
            var pieces = new List<ISquarePiece>() { TestHelpers.CreatePiece(0, 0, true), TestHelpers.CreatePiece(0, 1, true), TestHelpers.CreatePiece(1, 0, true), TestHelpers.CreatePiece(1, 1, true) };
            var xPos = new float[] { 1f, 1.5f };
            var yPos = new float[] { -0.5f, 0f };

            PieceManager.Instance.Setup(pieces, xPos, yPos);
        }

        [Test]
        public void NotEmptySlot_PieceExists()
        {
            Assert.IsFalse(PieceManager.Instance.IsEmptySlot(0, 0));
            Assert.IsFalse(PieceManager.Instance.HasEmptySlotInColumn(0, out int row));
        }

        [Test]
        public void NotEmptySlot_InvalidXY()
        {
            Assert.IsFalse(PieceManager.Instance.IsEmptySlot(3, 3));
        }
        
        [Test]
        public void EmptySlot_PieceInactive()
        {
            var pieces = new List<ISquarePiece>() { TestHelpers.CreatePiece(0, 0, true), TestHelpers.CreatePiece(0, 1, false), TestHelpers.CreatePiece(1, 0, true), TestHelpers.CreatePiece(1, 1, true) };
            var xPos = new float[] { 1f, 1.5f };
            var yPos = new float[] { -0.5f, 0f };

            PieceManager.Instance.Setup(pieces, xPos, yPos);

            Assert.IsTrue(PieceManager.Instance.IsEmptySlot(0, 1));
            Assert.IsTrue(PieceManager.Instance.HasEmptySlotInColumn(0, out int row));
            Assert.AreEqual(1, row);

        }

        public void EmptySlot_NullPiece()
        {
            var piece = PieceManager.Instance.GetPiece(0, 1);
            piece = null;

            Assert.IsTrue(PieceManager.Instance.IsEmptySlot(0, 1));
            Assert.IsTrue(PieceManager.Instance.HasEmptySlotInColumn(0, out int row));
            Assert.AreEqual(1, row);
        }

        [Test]
        public void NotEmptySlot_ContainsLockedPiece()
        {
            var lockedPiece = TestHelpers.CreatePiece(0, 0, true);
            lockedPiece.DestroyPieceHandler = new Scripts.Workers.Piece_Effects.Destruction.LockedSwap(lockedPiece);

            var pieces = new List<ISquarePiece>() { lockedPiece, TestHelpers.CreatePiece(0, 1, true), TestHelpers.CreatePiece(1, 0, true), TestHelpers.CreatePiece(1, 1, true) };
            var xPos = new float[] { 1f, 1.5f };
            var yPos = new float[] { -0.5f, 0f };

            PieceManager.Instance.Setup(pieces, xPos, yPos);

            Assert.IsFalse(PieceManager.Instance.IsEmptySlot(0, 0));
            Assert.IsFalse(PieceManager.Instance.HasEmptySlotInColumn(0, out int row));
        }

        [Test]
        public void EmptySlot_PieceBelowLockedMissing()
        {
            var lockedPiece = TestHelpers.CreatePiece(0, 0, true);
            lockedPiece.DestroyPieceHandler = new Scripts.Workers.Piece_Effects.Destruction.LockedSwap(lockedPiece);

            var pieces = new List<ISquarePiece>() { lockedPiece, TestHelpers.CreatePiece(0, 1, false), TestHelpers.CreatePiece(1, 0, true), TestHelpers.CreatePiece(1, 1, true) };
            var xPos = new float[] { 1f, 1.5f };
            var yPos = new float[] { -0.5f, 0f };

             PieceManager.Instance.Setup(pieces, xPos, yPos);

            Assert.IsTrue(PieceManager.Instance.IsEmptySlot(0, 0));
            Assert.IsTrue(PieceManager.Instance.HasEmptySlotInColumn(0, out int row));
            Assert.AreEqual(0, row);
        }

    }
}
