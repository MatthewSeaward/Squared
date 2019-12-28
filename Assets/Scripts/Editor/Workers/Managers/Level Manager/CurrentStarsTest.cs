using Assets.Scripts.Workers.Level_Info;
using NUnit.Framework;

namespace Assets.Scripts.Editor.Workers.Managers.Level_Manager
{
    [Category("Level Manager")]
    class CurrentStarsTest
    {
        [SetUp]
        public void Setup()
        {
            LevelManager.Instance.Levels.Clear();
        }

        [Test]
        public void Default_NoStars()
        {
            Assert.AreEqual(0, LevelManager.Instance.CurrentStars);
        }

        [Test]
        public void LevelsNoStars()
        {
            LevelManager.Instance.Levels.Add("Golem", new Level[] { GetLevel(0), GetLevel(0) });

            Assert.AreEqual(0, LevelManager.Instance.CurrentStars);
        }

        [Test]
        public void LevelsWithStars()
        {
            LevelManager.Instance.Levels.Add("Golem", new Level[] { GetLevel(1), GetLevel(2), GetLevel(4) });

            Assert.AreEqual(7, LevelManager.Instance.CurrentStars);
        }

        [Test]
        public void LevelWithoutProgress()
        {
            var noProgress = GetLevel(2);
            noProgress.LevelProgress = null;

            LevelManager.Instance.Levels.Add("Golem", new Level[] { GetLevel(1), noProgress, GetLevel(4) });

            Assert.AreEqual(5, LevelManager.Instance.CurrentStars);
        }

        [Test]
        public void NonNegative()
        {
            LevelManager.Instance.Levels.Add("Golem", new Level[] { GetLevel(-1), GetLevel(-2)});

            Assert.AreEqual(0, LevelManager.Instance.CurrentStars);
        }

        private Level GetLevel(int starsAchieved)
        {
            var lvl = new Level();

            lvl.LevelProgress = new Scripts.Workers.IO.Data_Entities.LevelProgress();
            lvl.LevelProgress.StarAchieved = starsAchieved;

            return lvl;
        }
    }
}
