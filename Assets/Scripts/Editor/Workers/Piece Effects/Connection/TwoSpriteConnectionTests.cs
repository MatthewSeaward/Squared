using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Piece_Connection;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            foreach (int sprite in System.Enum.GetValues(typeof(Colour)))
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
            var normalPiece = TestHelpers.CreatePiece(0, 0);
            normalPiece.PieceColour = Colour.Red;
            normalPiece.Sprite = TestHelpers.GetSprite(Colour.Red);
            normalPiece.PieceConnection = new StandardConnection();

            var fadePiece = TestHelpers.CreatePiece(1, 0);

            fadePiece.PieceConnection = new TwoSpriteConnection(Colour.Red);
            fadePiece.Sprite = TestHelpers.GetSprite(Colour.Orange);

            Assert.IsTrue(fadePiece.PieceConnection.ConnectionValid(fadePiece, normalPiece) || normalPiece.PieceConnection.ConnectionValid(normalPiece, fadePiece));
        }

        [Test]
        public void FadePiece_ToNormalNonMatchingPiece()
        {
            var normalPiece = TestHelpers.CreatePiece(0, 0);
            normalPiece.PieceColour = Colour.DarkBlue;
            normalPiece.Sprite = TestHelpers.GetSprite(Colour.DarkBlue);
            normalPiece.PieceConnection = new StandardConnection();

            var fadePiece = TestHelpers.CreatePiece(1, 0);

            fadePiece.PieceConnection = new TwoSpriteConnection(Colour.Red);
            fadePiece.Sprite = TestHelpers.GetSprite(Colour.Orange);

            Assert.IsFalse(fadePiece.PieceConnection.ConnectionValid(fadePiece, normalPiece) && normalPiece.PieceConnection.ConnectionValid(normalPiece, fadePiece));
        }

        [Test]
        public void FadePiece_ToFadeMatchingPiece()
        {
            var fadePiece1 = TestHelpers.CreatePiece(0, 0);

            fadePiece1.PieceConnection = new TwoSpriteConnection(Colour.Red);
            fadePiece1.Sprite = TestHelpers.GetSprite(Colour.Orange);

            var fadePiece2 = TestHelpers.CreatePiece(1, 0);

            fadePiece2.PieceConnection = new TwoSpriteConnection(Colour.Red);
            fadePiece2.Sprite = TestHelpers.GetSprite(Colour.Orange);

            Assert.IsTrue(fadePiece1.PieceConnection.ConnectionValid(fadePiece2, fadePiece1) || fadePiece1.PieceConnection.ConnectionValid(fadePiece1, fadePiece2));
        }

        [Test]
        public void FadePiece_ToFadePart1MatchingPiece()
        {
            var fadePiece1 = TestHelpers.CreatePiece(0, 0);

            fadePiece1.PieceConnection = new TwoSpriteConnection(Colour.DarkBlue);
            fadePiece1.Sprite = TestHelpers.GetSprite(Colour.Orange);

            var fadePiece2 = TestHelpers.CreatePiece(1, 0);

            fadePiece2.PieceConnection = new TwoSpriteConnection(Colour.Red);
            fadePiece2.Sprite = TestHelpers.GetSprite(Colour.Orange);

            Assert.IsTrue(fadePiece1.PieceConnection.ConnectionValid(fadePiece2, fadePiece1) || fadePiece1.PieceConnection.ConnectionValid(fadePiece1, fadePiece2));
        }

        [Test]
        public void FadePiece_ToFadePart2MatchingPiece()
        {
            var fadePiece1 = TestHelpers.CreatePiece(0, 0);

            fadePiece1.PieceConnection = new TwoSpriteConnection(Colour.Red);
            fadePiece1.Sprite = TestHelpers.GetSprite(Colour.DarkBlue);

            var fadePiece2 = TestHelpers.CreatePiece(1, 0);

            fadePiece2.PieceConnection = new TwoSpriteConnection(Colour.Red);
            fadePiece2.Sprite = TestHelpers.GetSprite(Colour.Orange);

            Assert.IsTrue(fadePiece1.PieceConnection.ConnectionValid(fadePiece2, fadePiece1) || fadePiece1.PieceConnection.ConnectionValid(fadePiece1, fadePiece2));
        }

        [Test]
        public void FadePiece_ToFadeNonMatchingPiece()
        {
            var fadePiece1 = TestHelpers.CreatePiece(0, 0);

            fadePiece1.PieceConnection = new TwoSpriteConnection(Colour.Green);
            fadePiece1.Sprite = TestHelpers.GetSprite(Colour.DarkBlue);

            var fadePiece2 = TestHelpers.CreatePiece(1, 0);

            fadePiece2.PieceConnection = new TwoSpriteConnection(Colour.Red);
            fadePiece2.Sprite = TestHelpers.GetSprite(Colour.Orange);

            Assert.IsTrue(fadePiece1.PieceConnection.ConnectionValid(fadePiece2, fadePiece1) && fadePiece1.PieceConnection.ConnectionValid(fadePiece1, fadePiece2));
        }
    }
}
