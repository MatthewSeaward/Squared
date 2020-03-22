using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Piece_Connection;
using NUnit.Framework;
using System;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.Editor.Workers.Piece_Effects.Connection
{
    [Category("Piece Effects")]
    public class NoConnectionTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            GameResources.PieceSprites.Clear();

            foreach (int sprite in Enum.GetValues(typeof(Colour)))
            {
                if (sprite < 0)
                {
                    continue;
                }
                GameResources.PieceSprites.Add(sprite.ToString(), TestHelpers.GetSprite(sprite.ToString()));
            }

            var go = new GameObject();
            var ps = go.AddComponent<PieceSelectionManager>();
            ps.Awake();
        }

        [Test]
        public void NoConnectionAdjancent_NormalPiece()
        {
            var piece1 = TestHelpers.CreatePiece(0, 0, Colour.Red);
            piece1.PieceConnection = new StandardConnection(piece1);

            var piece2 = TestHelpers.CreatePiece(1, 0);
            piece2.PieceConnection = new NoConnection();

            Assert.IsFalse(ConnectionHelper.ValidConnectionBetween(piece1, piece2));
        }


        [Test]
        public void NormalPieceToNoConnectionAdjancent()
        {
            var piece1 = TestHelpers.CreatePiece(0, 0);
            piece1.PieceConnection = new NoConnection();

            var piece2 = TestHelpers.CreatePiece(1, 0, Colour.Red);
            piece2.PieceConnection = new StandardConnection(piece2);

            Assert.IsFalse(ConnectionHelper.ValidConnectionBetween(piece1, piece2));
        }

        [Test]
        public void NoConnectionAdjancent_AnyAdjacentPiece()
        {
            var piece1 = TestHelpers.CreatePiece(0, 0);
            piece1.PieceConnection = new AnyAdjancentConnection(piece1);

            var piece2 = TestHelpers.CreatePiece(1, 0);
            piece2.PieceConnection = new NoConnection();

            Assert.IsFalse(ConnectionHelper.ValidConnectionBetween(piece1, piece2));
        }

        [Test]
        public void FadeAdjancent_AnyAdjacentPiece()
        {
            var piece1 = TestHelpers.CreatePiece(0, 0, Colour.Red);
            piece1.PieceConnection = new TwoSpriteConnection(piece1, Colour.DarkBlue);

            var piece2 = TestHelpers.CreatePiece(1, 0);
            piece2.PieceConnection = new NoConnection();

            Assert.IsFalse(ConnectionHelper.ValidConnectionBetween(piece1, piece2));
        }

        [Test]
        public void NonMatchingPieces()
        {
            var piece1 = TestHelpers.CreatePiece(0, 0, Colour.Red);
            piece1.PieceConnection = new StandardConnection(piece1);

            var piece2 = TestHelpers.CreatePiece(1, 0, Colour.DarkBlue);
            piece2.PieceConnection = new StandardConnection(piece2);

            Assert.IsFalse(ConnectionHelper.ValidConnectionBetween(piece1, piece2));
        }

        [Test]
        public void MatchingPieces_NonAdjancet()
        {
            var piece1 = TestHelpers.CreatePiece(0, 0, Colour.Red);
            piece1.PieceConnection = new StandardConnection(piece1);

            var piece2 = TestHelpers.CreatePiece(3, 0, Colour.Red);
            piece2.PieceConnection = new StandardConnection(piece2);

            Assert.IsFalse(ConnectionHelper.ValidConnectionBetween(piece1, piece2));
        }
    }
}
