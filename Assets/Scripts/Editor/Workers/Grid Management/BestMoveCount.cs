using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Grid_Management;
using Assets.Scripts.Workers.Piece_Effects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Workers.TestHelpers;

namespace Assets.Scripts.Editor.Workers
{
    [Category("Grid Management")]
    public class BestMoveCount
    {

        private IBestMoveChecker BestMoverChecker;

        [OneTimeSetUp]
        public void TestStart()
        {
            BestMoverChecker = new BestMoveDepthSearch();
        }

        [Test]
        public void BestMove_4x4_Best8()
        {
            var pieces = new string[]
            {
                "1001",
                "1101",
                "0101",
                "0010"
            };

            BuildGrid(pieces);

            Assert.AreEqual(8, BestMoverChecker.GetBestMove().Move.Count);
        }

        [Test]
        public void BestMove_4x4_Best4()
        {
            var pieces = new string[]
            {
                "1201",
                "1121",
                "0101",
                "2020"
            };

            BuildGrid(pieces);

            Assert.AreEqual(4, BestMoverChecker.GetBestMove().Move.Count);
        }

        [Test]
        public void BestMove_4X4_Best10()
        {
            var pieces = new string[]
            {
                "1111",
                "0010",
                "1100",
                "1110"
            };

            BuildGrid(pieces);

            Assert.AreEqual(10, BestMoverChecker.GetBestMove().Move.Count);
        }

        [Test]
        public void BestMove_4X4_Best6()
        {
            var pieces = new string[]
            {
                "0201",
                "4110",
                "1041",
                "1130"
            };

            BuildGrid(pieces);

            Assert.AreEqual(6, BestMoverChecker.GetBestMove().Move.Count);
        }

        [Test]
        public void BestMove_5X5_Best11()
        {
            var pieces = new string[]
            {
                "02101",
                "01310",
                "11002",
                "10213",
                "11100"
            };

            BuildGrid(pieces);

            Assert.AreEqual(11, BestMoverChecker.GetBestMove().Move.Count);
        }

        [Test]
        public void BestMove_2X2_AnyConnection_Best2()
        {
            var pieces = new string[]
            {
                "12",
                "r3"
             };

            BuildGrid(pieces);

            Assert.AreEqual(3, BestMoverChecker.GetBestMove().Move.Count);
        }

        [Test]
        public void BestMove_2X2_Best2()
        {
            var pieces = new string[]
            {
                "12",
                "13"
             };

            BuildGrid(pieces);

            Assert.AreEqual(2, BestMoverChecker.GetBestMove().Move.Count);
        }

        [Test]
        public void BestMove_3X3_Best4()
        {
            var pieces = new string[]
            {
                "120",
                "131",
                "413"
             };

            BuildGrid(pieces);

            Assert.AreEqual(4, BestMoverChecker.GetBestMove().Move.Count);
        }

        [Test]
        public void BestMove_3X3_Best3()
        {
            var pieces = new string[]
            {
                "121",
                "412",
                "131"
             };

            BuildGrid(pieces);

            Assert.AreEqual(3, BestMoverChecker.GetBestMove().Move.Count);
        }

        [Test]
        public void BestMove_3X3_AnyConnection_Best6()
        {
            var pieces = new string[]
            {
                "110",
                "2r3",
                "122"
            };

            BuildGrid(pieces);

            Assert.AreEqual(6, BestMoverChecker.GetBestMove().Move.Count);
        }

        private static void BuildGrid(string[] grid)
        {
            var list = new List<ISquarePiece>();

            Debug.Log("--Grid--");
            for (int y = 0; y < grid.Length; y++)
            {
                Debug.Log(grid[y]);

                for (int x = 0; x < grid[y].Length; x++)
                {
                    var piece = grid[y][x].ToString();
                    if (piece == "r")
                    {
                        list.Add(BuildAnyConnectionPiece(x, y, piece));
                    }
                    else
                    {
                        list.Add(BuildPiece(x, y, piece));
                    }
                }
            }
            Debug.Log("----");

            PieceController.Setup(list, new float[grid[0].Length], new float[grid.Length]);
        }

        private static ISquarePiece BuildPiece(int x, int y, string sprite)
        {
            var piece = CreatePiece();
            piece.Position = new Vector2Int(x, y);
            piece.Sprite = GetSprite(sprite);
            piece.PieceColour = (SquarePiece.Colour)Convert.ToInt32(sprite);
            piece.PieceConnection = new StandardConnection();
            piece.Scoring = new SingleScore();
            return piece;
        }

        private static ISquarePiece BuildAnyConnectionPiece(int x, int y, string sprite)
        {
            var piece = CreatePiece();
            piece.Position = new Vector2Int(x, y);
            piece.Sprite = GetSprite(sprite);
            piece.PieceColour = SquarePiece.Colour.None;
            piece.PieceConnection = new AnyAdjancentConnection();
            piece.Scoring = new SingleScore();
            return piece;
        }
    }
}
