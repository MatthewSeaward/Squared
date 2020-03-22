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
    public class AnyAdjancentConnectionTests
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
        public void PieceToAnyAdjancent()
        {
            var piece1 = TestHelpers.CreatePiece(0, 0, Colour.Red);
            piece1.PieceConnection = new StandardConnection();

            var piece2 = TestHelpers.CreatePiece(1, 0);
            piece2.PieceConnection = new AnyAdjancentConnection();

            Assert.IsTrue(ConnectionHelper.ValidConnectionBetween(piece1, piece2));
        }
       
        [Test]
        public void PieceToAnyAdjancent_NonAdjancet()
        {
            var piece1 = TestHelpers.CreatePiece(0, 0, Colour.Red);
            piece1.PieceConnection = new StandardConnection();

            var piece2 = TestHelpers.CreatePiece(3, 0);
            piece2.PieceConnection = new AnyAdjancentConnection();

            Assert.IsFalse(ConnectionHelper.ValidConnectionBetween(piece1, piece2));
        }
    }
}
