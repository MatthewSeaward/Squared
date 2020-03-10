using NUnit.Framework;
using static Assets.Scripts.Editor.Workers.Grid_Management.Helpers.BuildGridHelper;
using Assets.Scripts.Workers.Grid_Management;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Workers.Score_and_Limits;

namespace Assets.Scripts.Editor.Workers.Grid_Management
{
    [Category("Grid Management")]
    public class PathFindingTests
    {
        private IPathFinder PathFinder;

        [OneTimeSetUp]
        public void TestStart()
        {
            PathFinder = new AStarPathFinder();
        }

        [Test]
        public void PathFinding_4x4_ShortRoute()
        {
            var pieces = new string[]
            {
                "1001",
                "1101",
                "0101",
                "0010"
            };

            BuildGrid(pieces);

            var path = PathFinder.FindPath(new Vector2Int(0, 0), new Vector2Int(1, 2), new NoRestriction());

            LogPath(path);
            Assert.AreEqual(4, path.Count);
        }

        [Test]
        public void PathFinding_4x4_LongRoute()
        {
            var pieces = new string[]
            {
                "1001",
                "1101",
                "0101",
                "0010"
            };

            BuildGrid(pieces);

            var path = PathFinder.FindPath(new Vector2Int(0, 1), new Vector2Int(3, 0), new NoRestriction());

            LogPath(path);
            Assert.AreEqual(7, path.Count);
        }

        [Test]
        public void PathFinding_4x4_InvalidRoute()
        {
            var pieces = new string[]
            {
                "1001",
                "1101",
                "0101",
                "0010"
            };

            BuildGrid(pieces);

            var path = PathFinder.FindPath(new Vector2Int(0, 0), new Vector2Int(2, 0), new NoRestriction());

            LogPath(path);
            Assert.AreEqual(0, path.Count);
        }


        [Test]
        public void PathFinding_5x5_Route1()
        {
            var pieces = new string[]
            {
                "01100",
                "00010",
                "01001",
                "10110", 
                "10010"
            };

            BuildGrid(pieces);

            var path = PathFinder.FindPath(new Vector2Int(1, 0), new Vector2Int(2, 3), new NoRestriction());

            LogPath(path);
            Assert.AreEqual(6, path.Count);
        }

        [Test]
        public void PathFinding_5x5_Route2()
        {
            var pieces = new string[]
            {
                "01100",
                "00010",
                "01001",
                "10110",
                "10010"
            };

            BuildGrid(pieces);

            var path = PathFinder.FindPath(new Vector2Int(3, 4), new Vector2Int(0, 4), new NoRestriction());

            LogPath(path);
            Assert.AreEqual(6, path.Count);
        }

        [Test]
        public void PathFinding_3x3_DiagonalOnlyRestriction_NoPath()
        {
            var pieces = new string[]
            {
                "100",
                "100",
                "010"
            };

            BuildGrid(pieces);

            var path = PathFinder.FindPath(new Vector2Int(0, 0), new Vector2Int(1, 2), new DiagonalOnlyRestriction());

            LogPath(path);
            Assert.AreEqual(0, path.Count);
        }

        [Test]
        public void PathFinding_3x3_DiagonalOnlyRestriction_Path()
        {
            var pieces = new string[]
            {
                "100",
                "010",
                "001"
            };

            BuildGrid(pieces);

            var path = PathFinder.FindPath(new Vector2Int(0, 0), new Vector2Int(2, 2), new DiagonalOnlyRestriction());

            LogPath(path);
            Assert.AreEqual(3, path.Count);
        }
        
        [Test]
        public void PathFinding_3x3_DiagonalRestriction_NoPath()
        {
            var pieces = new string[]
            {
                "100",
                "100",
                "010"
            };

            BuildGrid(pieces);

            var path = PathFinder.FindPath(new Vector2Int(0, 0), new Vector2Int(1, 2), new DiagonalRestriction());

            LogPath(path);
            Assert.AreEqual(0, path.Count);
        }

        [Test]
        public void PathFinding_3x3_DiagonalRestriction_Path()
        {
            var pieces = new string[]
            {
                "100",
                "100",
                "110"
            };

            BuildGrid(pieces);

            var path = PathFinder.FindPath(new Vector2Int(0, 0), new Vector2Int(1, 2), new DiagonalRestriction());

            LogPath(path);
            Assert.AreEqual(4, path.Count);
        }

        [Test]
        public void PathFinding_3x3_Rainbow()
        {
            var pieces = new string[]
            {
                "1982",
                "1r72",
                "5426"
            };

            BuildGrid(pieces);

            var path = PathFinder.FindPath(new Vector2Int(0, 0), new Vector2Int(3, 0), new NoRestriction());

            LogPath(path);
            Assert.AreEqual(6, path.Count);
        }

        [Test]
        public void PathFinding_3x3_Fade()
        {
            var pieces = new string[]
            {
                "1982",
                "1f72",
                "5426"
            };

            BuildGrid(pieces);

            var path = PathFinder.FindPath(new Vector2Int(0, 0), new Vector2Int(3, 0), new NoRestriction());

            LogPath(path);
            Assert.AreEqual(6, path.Count);
        }

        private void LogPath(List<Vector2Int> path)
        {
            Debug.Log(string.Join(", ", path.Select(x => x.x + ":" + x.y)));
        }
    }
}
