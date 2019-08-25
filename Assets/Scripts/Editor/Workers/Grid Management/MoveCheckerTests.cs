using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Piece_Effects;
using NUnit.Framework;
using System.Collections.Generic;
using static Assets.Scripts.Workers.TestHelpers;

namespace Assets.Scripts.Editor.Workers
{
    [Category("Grid Management")]
    public class MoveCheckerTests
    {
        [Test]
        public void False_NoPieces()
        {
            var pieces = new List<ISquarePiece>();

            PieceController.Setup(pieces, new float[0], new float[0]);

            Assert.IsFalse(MoveChecker.AvailableMove());
        }

        [Test]
        public void True_TwoMatchingPieces_Adjancent()
        {
            var pieces = new List<ISquarePiece>();
            pieces.Add(BuildPiece(0, 0, "1"));
            pieces.Add(BuildPiece(1, 0, "1"));

            PieceController.Setup(pieces, new float[2], new float[1]);

            Assert.IsTrue(MoveChecker.AvailableMove());
        }

        [Test]
        public void True_TwoMatchingPieces_AboveBelow()
        {
            var pieces = new List<ISquarePiece>();
            pieces.Add(BuildPiece(0, 0, "1"));
            pieces.Add(BuildPiece(0, 1, "1"));

            PieceController.Setup(pieces, new float[1], new float[2]);

            Assert.IsTrue(MoveChecker.AvailableMove());
        }

        [Test]
        public void False_NoMatchingPieces_AboveBelow()
        {
            var pieces = new List<ISquarePiece>();
            pieces.Add(BuildPiece(0, 0, "1"));
            pieces.Add(BuildPiece(0, 1, "0"));

            PieceController.Setup(pieces, new float[1], new float[2]);

            Assert.IsFalse(MoveChecker.AvailableMove());
        }

        [Test]
        public void True_TwoMatchingPieces_Diagonal_TopRight()
        {
            var pieces = new List<ISquarePiece>();
            pieces.Add(BuildPiece(0, 0, "1"));
            pieces.Add(BuildPiece(0, 1, "2"));
            pieces.Add(BuildPiece(1, 0, "0"));
            pieces.Add(BuildPiece(1, 1, "1"));

            PieceController.Setup(pieces, new float[2], new float[2]);

            Assert.IsTrue(MoveChecker.AvailableMove());
        }

        [Test]
        public void False_NoTwoMatchingPieces_Diagonal_TopRight()
        {
            var pieces = new List<ISquarePiece>();
            pieces.Add(BuildPiece(0, 0, "2"));
            pieces.Add(BuildPiece(0, 1, "3"));
            pieces.Add(BuildPiece(1, 0, "0"));
            pieces.Add(BuildPiece(1, 1, "1"));

            PieceController.Setup(pieces, new float[2], new float[2]);

            Assert.IsFalse(MoveChecker.AvailableMove());
        }

        [Test]
        public void True_TwoMatchingPieces_Diagonal_BottomRight()
        {
            var pieces = new List<ISquarePiece>();
            pieces.Add(BuildPiece(0, 0, "2"));
            pieces.Add(BuildPiece(0, 1, "1"));
            pieces.Add(BuildPiece(1, 0, "1"));
            pieces.Add(BuildPiece(1, 1, "0"));

            PieceController.Setup(pieces, new float[2], new float[2]);

            Assert.IsTrue(MoveChecker.AvailableMove());
        }

        [Test]
        public void True_ThreePieces_TwoMatchingPieces_Adjancent()
        {
            var pieces = new List<ISquarePiece>();
            pieces.Add(BuildPiece(0, 0, "0"));
            pieces.Add(BuildPiece(1, 0, "1"));
            pieces.Add(BuildPiece(2, 0, "1"));

            PieceController.Setup(pieces, new float[3], new float[1]);

            Assert.IsTrue(MoveChecker.AvailableMove());
        }

        [Test]
        public void False_ThreePieces_NoMatchingPieces_Adjancent()
        {
            var pieces = new List<ISquarePiece>();
            pieces.Add(BuildPiece(0, 0, "0"));
            pieces.Add(BuildPiece(1, 0, "1"));
            pieces.Add(BuildPiece(2, 0, "0"));

            PieceController.Setup(pieces, new float[3], new float[1]);

            Assert.IsFalse(MoveChecker.AvailableMove());
        }

        [Test]
        public void True_TwoMatchingPieces_Adjancent_AnyConnection()
        {
            var pieces = new List<ISquarePiece>();
            pieces.Add(BuildPiece(0, 0, "1"));
            pieces.Add(BuildAnyConnectionPiece(1, 0, "2"));

            PieceController.Setup(pieces, new float[2], new float[1]);

            Assert.IsTrue(MoveChecker.AvailableMove());
        }

        [Test]
        public void True_TwoMatchingPieces_Diagonal_BottomRight_AnyConnection()
        {
            var pieces = new List<ISquarePiece>();
            pieces.Add(BuildPiece(0, 0, "2"));
            pieces.Add(BuildAnyConnectionPiece(0, 1, "1"));
            pieces.Add(BuildPiece(1, 0, "3"));
            pieces.Add(BuildPiece(1, 1, "0"));

            PieceController.Setup(pieces, new float[2], new float[2]);
        }

        private static ISquarePiece BuildPiece(int x, int y, string sprite)
        {
            var piece = CreatePiece();
            piece.Position = new UnityEngine.Vector2Int(x, y);
            piece.Sprite = GetSprite(sprite);
            piece.PieceConnection = new StandardConnection();
            return piece;
        }

        private static ISquarePiece BuildAnyConnectionPiece(int x, int y, string sprite)
        {
            var piece = CreatePiece();
            piece.Position = new UnityEngine.Vector2Int(x, y);
            piece.Sprite = GetSprite(sprite);
            piece.PieceConnection = new AnyAdjancentConnection();
            return piece;
        }
    }
}
