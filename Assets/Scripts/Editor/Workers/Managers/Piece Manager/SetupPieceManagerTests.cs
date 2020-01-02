using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Editor.Workers.Managers.Piece_Manager
{
    [Category("Manager")]
    class SetupPieceManagerTests
    {
        [Test]
        public void SetupPieceManager()
        {
            var pieces = new List<ISquarePiece>() { TestHelpers.CreatePiece(), TestHelpers.CreatePiece(), TestHelpers.CreatePiece() };
            var xPos = new float[] { 1f, 1.5f, 2f, 2.5f };
            var yPos = new float[] { -0.5f, 0f, 0.5f, 1f };

            PieceManager.Instance.Setup(pieces, xPos, yPos);

            Assert.AreEqual(pieces.Count, PieceManager.Instance.Pieces.Count);
            Assert.AreEqual(xPos.Length, PieceManager.Instance.XPositions.Length);
            Assert.AreEqual(xPos.Length, PieceManager.Instance.YPositions.Length);
        }

        [Test]
        public void SetupPieceManager_RowsAndColumnsTest()
        {
            var pieces = new List<ISquarePiece>() { TestHelpers.CreatePiece(), TestHelpers.CreatePiece(), TestHelpers.CreatePiece() };
            var xPos = new float[] { 1f, 1.5f, 2f, 2.5f };
            var yPos = new float[] { -0.5f, 0f, 0.5f, 1f, 1.5f };

            PieceManager.Instance.Setup(pieces, xPos, yPos);

            Assert.AreEqual(5, PieceManager.Instance.NumberOfRows);
            Assert.AreEqual(4, PieceManager.Instance.NumberOfColumns);
        }

        [Test]
        public void GetPosition_ValidXY()
        {
            var pieces = new List<ISquarePiece>() { TestHelpers.CreatePiece(0,0), TestHelpers.CreatePiece(0,1), TestHelpers.CreatePiece(1,0), TestHelpers.CreatePiece(1, 1) };
            var xPos = new float[] { 1f, 1.5f };
            var yPos = new float[] { -0.5f, 0f };

            PieceManager.Instance.Setup(pieces, xPos, yPos);

            Assert.AreEqual(new Vector3(1.5f, 0f), PieceManager.Instance.GetPosition(new Vector2Int(1, 1)));
        }

        [Test]
        public void GetPosition_InvalidXY()
        {
            var pieces = new List<ISquarePiece>() { TestHelpers.CreatePiece(0, 0), TestHelpers.CreatePiece(0, 1), TestHelpers.CreatePiece(1, 0), TestHelpers.CreatePiece(1, 1) };
            var xPos = new float[] { 1f, 1.5f};
            var yPos = new float[] { -0.5f, 0f};

            PieceManager.Instance.Setup(pieces, xPos, yPos);

            Assert.Throws<System.IndexOutOfRangeException>(() => PieceManager.Instance.GetPosition(new Vector2Int(3, 3)));
        }
    }
}
