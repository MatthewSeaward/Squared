using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Grid_Management;
using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Workers.TestHelpers;

namespace Assets.Scripts.Editor.Workers
{
    [Category("Grid Management")]
    public class BestMoveRestrictions
    {

        private IBestMoveChecker BestMoverChecker;

        [OneTimeSetUp]
        public void TestStart()
        {
            BestMoverChecker = new BestMoveDepthSearch();
            TestHelpers.CreatedSprites.Clear();
        }

        [Test]
        public void NoDiagonalRestriction_Best4()
        {
            IRestriction restriction = new DiagonalRestriction();

            var pieces = new string[]
            {
                "1--1",
                "11--",
                "-1-1",
                "--1-"
            };

            BuildGrid(pieces);

            var move = BestMoverChecker.GetBestMove(restriction);
            Assert.AreEqual(4, move.Count);
            Assert.IsFalse(restriction.IsRestrictionViolated(move.ToArray()));
        }

        [Test]
        public void DiagonalOnlyRestriction_Best4()
        {
            IRestriction restriction = new DiagonalOnlyRestriction();

            var pieces = new string[]
            {
                "1--1",
                "11--",
                "-1-1",
                "--1-"
            };

            BuildGrid(pieces);

            var move = BestMoverChecker.GetBestMove(restriction);
            Assert.AreEqual(4, move.Count);
            Assert.IsFalse(restriction.IsRestrictionViolated(move.ToArray()));
        }

        [Test]
        public void MinLimitRestriction_ShouldFindNone()
        {
            IRestriction restriction = new MinSequenceLimit(7);

            var pieces = new string[]
            {
                "1--1",
                "11--",
                "-1-1",
                "--1-"
            };

            BuildGrid(pieces);

            Assert.AreEqual(0, BestMoverChecker.GetBestMove(restriction).Count);
        }

        [Test]
        public void MaxLimitRestriction_Best4()
        {
            IRestriction restriction = new MaxSequenceLimit(4);

            var pieces = new string[]
            {
                "1--1",
                "11--",
                "-1-1",
                "--1-"
            };

            BuildGrid(pieces);

            var move = BestMoverChecker.GetBestMove(restriction);
            Assert.AreEqual(4, move.Count);
            Assert.IsFalse(restriction.IsRestrictionViolated(move.ToArray()));
        }

        [Test]
        public void BannedSprite1Restriction_Best4()
        {
            IRestriction restriction = new Assets.Scripts.Workers.Score_and_Limits.BannedSprite(1);

            var pieces = new string[]
            {
                "1-21",
                "112-",
                "-121",
                "--1-"
            };

            BuildGrid(pieces);

            var move = BestMoverChecker.GetBestMove(restriction);
            Assert.AreEqual(3, move.Count);
            Assert.IsFalse(restriction.IsRestrictionViolated(move.ToArray()));
        }

        [Test]
        public void BannedSprite2Restriction_WithAnyConnection_Best4()
        {
            IRestriction restriction = new Assets.Scripts.Workers.Score_and_Limits.BannedSprite(2);

            var pieces = new string[]
            {
                "1222",
                "11a2",
                "2222",
                "----"
            };

            BuildGrid(pieces);

            var move = BestMoverChecker.GetBestMove(restriction);
            Assert.AreEqual(4, move.Count);
            Assert.IsFalse(restriction.IsRestrictionViolated(move.ToArray()));
        }

        [Test]
        public void PieceTypeRestriction_Best5()
        {
            IRestriction restriction = new BannedPieceType("r");

            var pieces = new string[]
            {
                "1--1",
                "r1--",
                "-1-1",
                "--1-"
            };

            BuildGrid(pieces);

            var move = BestMoverChecker.GetBestMove(restriction);
            Assert.AreEqual(5, move.Count);
            Assert.IsFalse(restriction.IsRestrictionViolated(move.ToArray()));
        }

        [Test]
        public void LockedPiecesRestriction_Best2()
        {
            IRestriction restriction = new Assets.Scripts.Workers.Score_and_Limits.SwapEffectLimit();

            var pieces = new string[]
            {
                "L11L"
            };

            BuildGrid(pieces);

            var move = BestMoverChecker.GetBestMove(restriction);
            Assert.AreEqual(2, move.Count);
            Assert.IsFalse(restriction.IsRestrictionViolated(move.ToArray()));
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
                    if (piece == "-")
                    {
                        list.Add(BuildRandomPiece(x, y));
                    }
                    else if (piece == "r")
                    {
                        var p = BuildPiece(x, y, "1");
                        p.Type = PieceFactory.PieceTypes.Rainbow;
                        list.Add(p);
                    }
                    else if (piece == "L")
                    {
                        var p = BuildPiece(x, y, "1");
                        p.DestroyPieceHandler = new Scripts.Workers.Piece_Effects.Destruction.LockedSwap(p);
                        list.Add(p);
                    }
                    else if (piece == "a")
                    {
                        var p = BuildPiece(x, y, "1");
                        p.PieceConnection = new AnyAdjancentConnection();
                        list.Add(p);
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

        private static ISquarePiece BuildRandomPiece(int x, int y)
        {
            var piece = CreatePiece();
            piece.Position = new Vector2Int(x, y);
            piece.PieceColour = (SquarePiece.Colour)UnityEngine.Random.Range(3, 9);

            piece.Sprite = GetSprite(piece.PieceColour.ToString());
            piece.PieceConnection = new StandardConnection();
            piece.Scoring = new SingleScore(0);

            return piece;
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

        
    }
}
