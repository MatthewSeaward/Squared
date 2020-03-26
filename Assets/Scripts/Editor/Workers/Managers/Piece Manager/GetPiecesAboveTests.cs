using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Editor.Workers.Managers.Piece_Manager
{
    [Category("Manager")]
    class GetPiecesAboveTests
    {
        [SetUp]
        public void Setup()
        {
            var pieces = new List<ISquarePiece>() { TestHelpers.CreatePiece(0, 0), TestHelpers.CreatePiece(0, 1), TestHelpers.CreatePiece(0, 2) };
            var xPos = new float[] { 1f, 1.5f, 2f, 2.5f };
            var yPos = new float[] { -0.5f, 0f, 0.5f, 1f };

            PieceManager.Instance.Setup(pieces, xPos, yPos);
        }

        [Test]
        public void NoPiecesAbove()
        {
            Assert.AreEqual(0, PieceManager.Instance.GetPiecesAbove(0, 0).Length);
        }

        [Test]
        public void OnePieceAbove()
        {
            Assert.AreEqual(1, PieceManager.Instance.GetPiecesAbove(0, 1).Length);
        }

        [Test]
        public void TwoPiecesAbove()
        {
            Assert.AreEqual(2, PieceManager.Instance.GetPiecesAbove(0, 2).Length);
        }

        [Test]
        public void BottomPiece()
        {
            var pieces = new List<ISquarePiece>() { TestHelpers.CreatePiece(0, 0), TestHelpers.CreatePiece(0, 1), TestHelpers.CreatePiece(0, 2) };
            var xPos = new float[] { 1f, 1.5f, 2f };
            var yPos = new float[] { -0.5f, 0f, 0.5f };

            PieceManager.Instance.Setup(pieces, xPos, yPos);

            Assert.AreEqual(2, PieceManager.Instance.GetPiecesAbove(0, 2).Length);
        }

        [Test]
        public void SingleColumn()
        {
            var pieces = new List<ISquarePiece>() { TestHelpers.CreatePiece(0, 0), TestHelpers.CreatePiece(0, 1), TestHelpers.CreatePiece(0, 2) };
            var xPos = new float[] { 1f };
            var yPos = new float[] { -0.5f, 0f, 0.5f };

            PieceManager.Instance.Setup(pieces, xPos, yPos);

            Assert.AreEqual(2, PieceManager.Instance.GetPiecesAbove(0, 2).Length);
        }

        [Test]
        public void GetPieceAbove_SlotDoesNotExist()
        {
            // Add the piece but not the slot.
            PieceManager.Instance.Pieces.Add(TestHelpers.CreatePiece(1, 1));

            Assert.AreEqual(0, PieceManager.Instance.GetPiecesAbove(1, 2).Length);
        }

        [Test]
        public void NegativeYPosition()
        {
            // Add the piece but not the slot.
            PieceManager.Instance.AddNewPiece(TestHelpers.CreatePiece(1, -1));

            Assert.AreEqual(0, PieceManager.Instance.GetPiecesAbove(1, 2).Length);
        }

        [Test]
        public void NegativeXPosition()
        {
            // Add the piece but not the slot.
            PieceManager.Instance.AddNewPiece(TestHelpers.CreatePiece(-1, 1));

            Assert.AreEqual(0, PieceManager.Instance.GetPiecesAbove(-1, 2).Length);
        }
    }
}
