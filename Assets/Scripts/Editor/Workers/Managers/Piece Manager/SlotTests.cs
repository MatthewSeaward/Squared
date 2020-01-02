using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Editor.Workers.Managers.Piece_Manager
{
    [Category("Manager")]
    class SlotTests
    {
        [SetUp]
        public void Setup()
        {
            var pieces = new List<ISquarePiece>() { TestHelpers.CreatePiece(0,0), TestHelpers.CreatePiece(0,1), TestHelpers.CreatePiece(1,0), TestHelpers.CreatePiece(1, 1) };
            var xPos = new float[] { 1f, 1.5f };
            var yPos = new float[] { -0.5f, 0f };

            PieceManager.Instance.Setup(pieces, xPos, yPos);
        }

        [Test]
        public void SlotExists_ValidSlot_Default()
        {
            Assert.IsTrue(PieceManager.Instance.SlotExists(new Vector2Int(0, 0)));
        }

        [Test]
        public void SlotExists_ValidSlot_Ends()
        {
            Assert.IsTrue(PieceManager.Instance.SlotExists(new Vector2Int(1, 1)));
        }

        [Test]
        public void SlotExists_InvalidSlot_TooHigh()
        {
            Assert.IsFalse(PieceManager.Instance.SlotExists(new Vector2Int(2, 2)));
        }

        [Test]
        public void SlotExists_InvalidSlot_TooLow()
        {
            Assert.IsFalse(PieceManager.Instance.SlotExists(new Vector2Int(-1, -1)));
        }

        [Test]
        public void SlotExists_InvalidSlot_RemoveSlot()
        {
            Assert.IsTrue(PieceManager.Instance.SlotExists(new Vector2Int(1, 0)));

            PieceManager.Instance.RemoveSlot(new Vector2Int(1, 0));

            Assert.IsFalse(PieceManager.Instance.SlotExists(new Vector2Int(1, 0)));
        }

        [Test]
        public void SlotExists_ValidSlot_AddNewSlot()
        { 
            Assert.IsFalse(PieceManager.Instance.SlotExists(new Vector2Int(8, 8)));

            PieceManager.Instance.AddNewPiece(TestHelpers.CreatePiece(8, 8));

            Assert.IsTrue(PieceManager.Instance.SlotExists(new Vector2Int(8, 8)));
        }
    }
}
