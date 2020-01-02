using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Editor.Workers.Managers.Piece_Manager
{
    [Category("Manager")]
    class HasSlotInColumnTests
    {

        [Test]
        public void HasSlotInColumn()
        {
            var pieces = new List<ISquarePiece>() { TestHelpers.CreatePiece(0, 0), TestHelpers.CreatePiece(0, 1), TestHelpers.CreatePiece(1, 0), TestHelpers.CreatePiece(1, 1) };
            var xPos = new float[] { 1f, 1.5f };
            var yPos = new float[] { -0.5f, 0f };

            PieceManager.Instance.Setup(pieces, xPos, yPos);

            Assert.IsTrue(PieceManager.Instance.HasSlotInColumn(0));
        }

        [Test]
        public void DoesNotHaveSlotInColumn()
        {
            var pieces = new List<ISquarePiece>() { TestHelpers.CreatePiece(0, 0), TestHelpers.CreatePiece(0, 1), TestHelpers.CreatePiece(1, 0), TestHelpers.CreatePiece(1, 1) };
            var xPos = new float[] { 1f, 1.5f };
            var yPos = new float[] { -0.5f, 0f };

            PieceManager.Instance.Setup(pieces, xPos, yPos);

            Assert.IsFalse(PieceManager.Instance.HasSlotInColumn(3));
        }
    }
}
