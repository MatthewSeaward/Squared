using Assets.Scripts.Workers.Grid_Management;
using NUnit.Framework;
using static Assets.Scripts.Editor.Workers.Grid_Management.Helpers.BuildGridHelper;

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
    }
}