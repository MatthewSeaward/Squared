using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Score_and_Limits;
using NUnit.Framework;
using System;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;

namespace Assets.Scripts.Editor.Workers.Factorys
{
    [Category("Factorys")]
    class LevelStarFactoryTests
    {
        [Test]
        public void CorrectStarNumber()
        {
            var sut = LevelStarFactory.GetLevelStar(2, null, null);

            Assert.AreEqual(2, sut.Number);
        }

        [Test]
        public void Limit_Turns()
        {
            var sut = LevelStarFactory.GetLevelStar(2, new string[] { "Turns", "5" }, null);

            Assert.AreEqual(typeof(TurnLimit), sut.Limit.GetType());
            Assert.AreEqual("5 turns", (sut.Limit as TurnLimit).GetDescription());
        }

        [Test]
        public void Limit_Time()
        {
            var sut = LevelStarFactory.GetLevelStar(2, new string[] { "Time", "5" }, null);

            Assert.AreEqual(typeof(TimeLimit), sut.Limit.GetType());
            Assert.AreEqual("5 seconds", (sut.Limit as TimeLimit).GetDescription());
        }

        [Test]
        public void Limit_EmptyData()
        {
            var sut = LevelStarFactory.GetLevelStar(2, new string[] { "", "" }, null);

            Assert.IsNull(sut.Limit);
        }

        [Test]
        public void Limit_NoData()
        {
            var sut = LevelStarFactory.GetLevelStar(2, null, null);

            Assert.IsNull(sut.Limit);
        }

        [Test]
        public void Restriction_Min()
        {
            var sut = LevelStarFactory.GetLevelStar(2, new string[] { "", "", "Min", "2" }, null);

            Assert.AreEqual(typeof(MinSequenceLimit), sut.Restriction.GetType());
            Assert.AreEqual(2, (sut.Restriction as MinSequenceLimit).MinLimit);
        }

        [Test]
        public void Restriction_Min_InvalidFormat()
        {
            Assert.Throws<FormatException>(() => LevelStarFactory.GetLevelStar(2, new string[] { "", "", "Min", "Two" }, null));
        }

        [Test]
        public void Restriction_Max()
        {
            var sut = LevelStarFactory.GetLevelStar(2, new string[] { "", "", "Max", "3" }, null);

            Assert.AreEqual(typeof(MaxSequenceLimit), sut.Restriction.GetType());
            Assert.AreEqual(3, (sut.Restriction as MaxSequenceLimit).MaxLimit);
        }

        [Test]
        public void Restriction_Max_InvalidFormat()
        {
            Assert.Throws<FormatException>(() => LevelStarFactory.GetLevelStar(2, new string[] { "", "", "Max", "Two" }, null));
        }
        
        [Test]
        public void Restriction_Bannned()
        {
            var sut = LevelStarFactory.GetLevelStar(2, new string[] { "", "", "Banned", "1" }, null);

            Assert.AreEqual(typeof(Scripts.Workers.Score_and_Limits.BannedSprite), sut.Restriction.GetType());
            Assert.AreEqual(1, (sut.Restriction as Scripts.Workers.Score_and_Limits.BannedSprite).SpriteValue);
        }

        [Test]
        public void Restriction_Bannned_InvalidFormat()
        {
            Assert.Throws<FormatException>(() => LevelStarFactory.GetLevelStar(2, new string[] { "", "", "Banned", "Red" }, null));
        }

        [Test]
        public void Restriction_Type()
        {
            var sut = LevelStarFactory.GetLevelStar(2, new string[] { "", "", "Type", "r" }, null);

            Assert.AreEqual(typeof(BannedPieceType), sut.Restriction.GetType());
            Assert.AreEqual(PieceTypes.Rainbow, (sut.Restriction as BannedPieceType).BannedPiece);
        }

        [Test]
        public void Restriction_BannedLocked()
        {
            var sut = LevelStarFactory.GetLevelStar(2, new string[] { "", "", "Banned Locked", ""}, null);

            Assert.AreEqual(typeof(Assets.Scripts.Workers.Score_and_Limits.SwapEffectLimit), sut.Restriction.GetType());
        }

        [Test]
        public void Restriction_NoDiagonal()
        {
            var sut = LevelStarFactory.GetLevelStar(2, new string[] { "", "", "No Diagonal", ""}, null);

            Assert.AreEqual(typeof(DiagonalRestriction), sut.Restriction.GetType());
        }

        [Test]
        public void Restriction_DiagonalOnly()
        {
            var sut = LevelStarFactory.GetLevelStar(2, new string[] { "", "", "Diagonal Only", ""}, null);

            Assert.AreEqual(typeof(DiagonalOnlyRestriction), sut.Restriction.GetType());
        }

        [Test]
        public void Restriction_TurnTimeLimit()
        {
            var sut = LevelStarFactory.GetLevelStar(2, new string[] { "", "", "Time", "6" }, null);

            Assert.AreEqual(typeof(TurnTimeLimit), sut.Restriction.GetType());
        }

        [Test]
        public void Restriction_TurnTimeLimit_InvalidFormat()
        {
            Assert.Throws<FormatException>(() => LevelStarFactory.GetLevelStar(2, new string[] { "", "", "Time", "Two" }, null));
        }

        [Test]
        public void Restriction_Invalid()
        {
            var sut = LevelStarFactory.GetLevelStar(2, new string[] { "", "", "Sweets", "" }, null);

            Assert.AreEqual(typeof(Assets.Scripts.Workers.Score_and_Limits.NoRestriction), sut.Restriction.GetType());
        }

        [Test]
        public void Restriction_NoValues()
        {
            var sut = LevelStarFactory.GetLevelStar(2, new string[] { "", "" }, null);

            Assert.AreEqual(typeof(Assets.Scripts.Workers.Score_and_Limits.NoRestriction), sut.Restriction.GetType());
        }

        [Test]
        public void Events()
        {

            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType="Destroy", NumberOfPiecesToSelect=2} } } };

            var sut = LevelStarFactory.GetLevelStar(2, null, testEvent);

            Assert.IsNotNull(sut.Events);
        }
    }
}
