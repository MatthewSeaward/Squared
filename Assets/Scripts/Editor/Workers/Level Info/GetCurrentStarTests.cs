using Assets.Scripts.Workers.Level_Info;
using NUnit.Framework;

namespace Assets.Scripts.Editor.Workers.Level_Info
{
    [Category("Level Info")]
    class GetCurrentStarTests
    {
        Level sut = new Level();

        [SetUp]
        public void Setup()
        {
            sut.Star1Progress = new Scripts.Workers.IO.Data_Entities.StarProgress() { Number = 1 };
            sut.Star2Progress = new Scripts.Workers.IO.Data_Entities.StarProgress() { Number = 2 };
            sut.Star3Progress = new Scripts.Workers.IO.Data_Entities.StarProgress() { Number = 3 };
        }

        [Test]
        public void NoProgress_Star1()
        {
            sut.LevelProgress = null;

            Assert.AreEqual(1, sut.GetCurrentStar().Number);
        }

        [Test]
        public void CompletedStar1_OnStar2()
        {
            sut.LevelProgress = new Scripts.Workers.IO.Data_Entities.LevelProgress() { StarAchieved = 1 };

            Assert.AreEqual(2, sut.GetCurrentStar().Number);
        }

        [Test]
        public void CompletedStar2_OnStar3()
        {
            sut.LevelProgress = new Scripts.Workers.IO.Data_Entities.LevelProgress() { StarAchieved = 2 };

            Assert.AreEqual(3, sut.GetCurrentStar().Number);
        }

        [Test]
        public void ExceededStarCount_OnStar3()
        {
            sut.LevelProgress = new Scripts.Workers.IO.Data_Entities.LevelProgress() { StarAchieved = 5 };

            Assert.AreEqual(3, sut.GetCurrentStar().Number);
        }

        [Test]
        public void UnderStarCount_OnStar1()
        {
            sut.LevelProgress = new Scripts.Workers.IO.Data_Entities.LevelProgress() { StarAchieved = -5 };

            Assert.AreEqual(1, sut.GetCurrentStar().Number);
        }
    }
}
