using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using NUnit.Framework;
using System.Collections.Generic;

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
    }
}
