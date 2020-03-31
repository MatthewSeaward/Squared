using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Editor.Workers.Managers.Piece_Manager
{
    [Category("Manager")]
    class AddPieceTests
    {
        [SetUp]
        public void Setup()
        {
            var pieces = new List<ISquarePiece>() { TestHelpers.CreatePiece(0, 0, true), TestHelpers.CreatePiece(0, 1, true), TestHelpers.CreatePiece(1, 0, true), TestHelpers.CreatePiece(1, 1, true) };
            var xPos = new float[] { 0f, 1f, 2f };
            var yPos = new float[] { 0f, 1f, 2f };

            PieceManager.Instance.Setup(pieces, xPos, yPos);
        }

        [Test]
        public void AddPiece()
        {
            var piece = TestHelpers.CreatePiece(2, 1);

            PieceManager.Instance.AddNewPiece(piece);

            Assert.IsNotNull(PieceManager.Instance.GetPiece(2, 1));
        }

        [Test]
        public void AddPiece_PieceAlreadyExistsInSpot()
        {
            var piece = TestHelpers.CreatePiece(1, 1);

            Assert.Throws<InvalidOperationException>(() => PieceManager.Instance.AddNewPiece(piece));
        }
    }
}
