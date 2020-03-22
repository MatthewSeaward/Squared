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
    public class TwoSpriteConnectionTests
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
        public void FadePiece_ToNormalMatchingPiece()
        {
            var normalPiece = TestHelpers.CreatePiece(0, 0, Colour.Red);
            normalPiece.PieceConnection = new StandardConnection();

            var fadePiece = TestHelpers.CreatePiece(1, 0, Colour.Orange);
            fadePiece.PieceConnection = new TwoSpriteConnection(Colour.Red);

            Assert.IsTrue(ConnectionHelper.ValidConnectionBetween(normalPiece, fadePiece));
        }

        [Test]
        public void FadePiece_ToNormalNonMatchingPiece()
        {
            var normalPiece = TestHelpers.CreatePiece(0, 0, Colour.DarkBlue);
            normalPiece.PieceConnection = new StandardConnection();

            var fadePiece = TestHelpers.CreatePiece(1, 0, Colour.Orange);
            fadePiece.PieceConnection = new TwoSpriteConnection(Colour.Red);

            Assert.IsFalse(ConnectionHelper.ValidConnectionBetween(normalPiece, fadePiece));
        }

        [Test]
        public void FadePiece_ToFadeMatchingPiece()
        {
            var fadePiece1 = TestHelpers.CreatePiece(0, 0, Colour.Orange);
            fadePiece1.PieceConnection = new TwoSpriteConnection(Colour.Red);

            var fadePiece2 = TestHelpers.CreatePiece(1, 0, Colour.Orange);
            fadePiece2.PieceConnection = new TwoSpriteConnection(Colour.Red);

            Assert.IsTrue(ConnectionHelper.ValidConnectionBetween(fadePiece1, fadePiece2));
        }

        [Test]
        public void FadePiece_ToFadePart1MatchingPiece()
        {
            var fadePiece1 = TestHelpers.CreatePiece(0, 0, Colour.Orange);
            fadePiece1.PieceConnection = new TwoSpriteConnection(Colour.DarkBlue);

            var fadePiece2 = TestHelpers.CreatePiece(1, 0, Colour.Orange);
            fadePiece2.PieceConnection = new TwoSpriteConnection(Colour.Red);

            Assert.IsTrue(ConnectionHelper.ValidConnectionBetween(fadePiece1, fadePiece2));
        }

        [Test]
        public void FadePiece_ToFadePart2MatchingPiece()
        {
            var fadePiece1 = TestHelpers.CreatePiece(0, 0, Colour.DarkBlue);
            fadePiece1.PieceConnection = new TwoSpriteConnection(Colour.Red);

            var fadePiece2 = TestHelpers.CreatePiece(1, 0, Colour.Orange);
            fadePiece2.PieceConnection = new TwoSpriteConnection(Colour.Red);

            Assert.IsTrue(ConnectionHelper.ValidConnectionBetween(fadePiece1, fadePiece2));
        }

        [Test]
        public void FadePiece_ToFadePart1To2MatchingPiece()
        {
            var fadePiece1 = TestHelpers.CreatePiece(0, 0, Colour.DarkBlue);
            fadePiece1.PieceConnection = new TwoSpriteConnection(Colour.Red);

            var fadePiece2 = TestHelpers.CreatePiece(1, 0, Colour.Red);
            fadePiece2.PieceConnection = new TwoSpriteConnection(Colour.Orange);

            Assert.IsTrue(ConnectionHelper.ValidConnectionBetween(fadePiece1, fadePiece2));
        }

        [Test]
        public void FadePiece_ToFadePart2To1MatchingPiece()
        {
            var fadePiece1 = TestHelpers.CreatePiece(0, 0, Colour.DarkBlue);
            fadePiece1.PieceConnection = new TwoSpriteConnection(Colour.Red);

            var fadePiece2 = TestHelpers.CreatePiece(1, 0, Colour.DarkBlue);
            fadePiece2.PieceConnection = new TwoSpriteConnection(Colour.Orange);

            Assert.IsTrue(ConnectionHelper.ValidConnectionBetween(fadePiece1, fadePiece2));
        }


        [Test]
        public void FadePiece_ToFadeNonMatchingPiece()
        {
            var fadePiece1 = TestHelpers.CreatePiece(0, 0, Colour.DarkBlue);
            fadePiece1.PieceConnection = new TwoSpriteConnection(Colour.Green);

            var fadePiece2 = TestHelpers.CreatePiece(1, 0, Colour.Orange);
            fadePiece1.PieceConnection = new TwoSpriteConnection(Colour.Red);

            Assert.IsFalse(ConnectionHelper.ValidConnectionBetween(fadePiece1, fadePiece2));
        }
    }
}
