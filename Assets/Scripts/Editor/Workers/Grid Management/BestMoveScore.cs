using Assets.Scripts.Workers.Grid_Management;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Piece_Effects;
using Assets.Scripts.Workers.Piece_Effects.Piece_Connection;
using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Workers.Helpers.TestHelpers;

namespace Assets.Scripts.Editor.Workers
{
    [Category("Grid Management")]
    public class BestMoveScore
    {

        private IBestMoveChecker BestMoverChecker;
        private IScoreCalculator scoreCalculator;

        [OneTimeSetUp]
        public void TestStart()
        {
            BestMoverChecker = new BestMoveDepthSearch();
            scoreCalculator = new StandardScoreCalculator();
            Constants.BonusPoints.Points = new List<(int, int)>();
        }

        [Test]
        public void BestScore_3x3_JustDoubles_Score0()
        {
            var pieces = new string[]
            {
                "dd-",
                "--d",
                "---"
            };

            BuildGrid(pieces);

            Assert.AreEqual(0, scoreCalculator.CalculateScore(BestMoverChecker.GetBestMove().Move.ToArray()));
        }

        [Test]
        public void BestScore_4x4_DoubleDoubleScore_Score16()
        {
            var pieces = new string[]
            {
                "-21",
                "-1d",
                "d--",
                "---"
            };

            BuildGrid(pieces);

            Assert.AreEqual(16, scoreCalculator.CalculateScore(BestMoverChecker.GetBestMove().Move.ToArray()));
        }

        [Test]
        public void BestScore_3x3_DoubleScore_Score6()
        {
            var pieces = new string[]
            {
                "2--",
                "1--",
                "-d-"
            };

            BuildGrid(pieces);

            Assert.AreEqual(6, scoreCalculator.CalculateScore(BestMoverChecker.GetBestMove().Move.ToArray()));
        }

        [Test]
        public void BestScore_5x5_Mixed_ThreeOptionsOneLowScore_Score7()
        {
            var pieces = new string[]
            {
                "2-1--",
                "-3---",
                "----1",
                "13--1",
                "3--11-"
            };

            BuildGrid(pieces);

            Assert.AreEqual(7, scoreCalculator.CalculateScore(BestMoverChecker.GetBestMove().Move.ToArray()));
        }

        [Test]
        public void BestScore_3x3_Mixed_TwoOptionsOneLowScore_Score5()
        {
            var pieces = new string[]
            {
                "111",
                "---",
                "33-"
            };

            BuildGrid(pieces);

            Assert.AreEqual(6, scoreCalculator.CalculateScore(BestMoverChecker.GetBestMove().Move.ToArray()));
        }

        [Test]
        public void BestScore_Mixed_Score7()
        {
            var pieces = new string[]
            {
                "12-",
                "-1-",
                "--3"
            };

            BuildGrid(pieces);

            Assert.AreEqual(7, scoreCalculator.CalculateScore(BestMoverChecker.GetBestMove().Move.ToArray()));
        }

        [Test]
        public void BestScore_All1_Score3()
        {
            var pieces = new string[]
            {
                "1--",
                "-1-",
                "1--"
            };

            BuildGrid(pieces);

            Assert.AreEqual(3, scoreCalculator.CalculateScore(BestMoverChecker.GetBestMove().Move.ToArray()));
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
                    else if (piece == "d")
                    {
                        var p = BuildPiece(x, y, "1");
                        p.Scoring = new MultipliedScore(2);
                        list.Add(p);
                    }
                    else
                    {
                        list.Add(BuildPiece(x, y, piece));
                    }
                }
            }
            Debug.Log("----");

            PieceManager.Instance.Setup(list, new float[grid[0].Length], new float[grid.Length]);
        }

        private static ISquarePiece BuildRandomPiece(int x, int y)
        {
            var piece = CreatePiece();
            piece.Position = new Vector2Int(x, y);
            piece.PieceColour = (SquarePiece.Colour)UnityEngine.Random.Range(1, 9);

            piece.Sprite = GetSprite(piece.PieceColour.ToString());
            piece.PieceConnection = new StandardConnection(piece);
            piece.Scoring = new SingleScore(0);

            return piece;
        }    

        private static ISquarePiece BuildPiece(int x, int y, string sprite)
        {
            var piece = CreatePiece();
            piece.Position = new Vector2Int(x, y);
            piece.Sprite = GetSprite("0");
            piece.PieceColour = SquarePiece.Colour.Red;
            piece.PieceConnection = new StandardConnection(piece);

            int value = Convert.ToInt32(sprite);
            piece.Scoring = new SingleScore(value);

            return piece;
        }
    }
}
