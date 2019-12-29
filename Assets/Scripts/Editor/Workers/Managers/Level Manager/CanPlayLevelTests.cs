using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Level_Info;
using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Scripts.Editor.Workers.Managers.Level_Manager
{
    [Category("Level Manager")]
    class CanPlayLevelTests
    {
        [SetUp]
        public void Setup()
        {
            LevelManager.Instance.Levels.Clear();
            LevelManager.Instance.SetupLevels(new Dictionary<string, Level[]>(), new string[] { "Golem" });
            LevelManager.Instance.SetChapter("Golem");

            LivesManager.Instance.Reset();

            DebugHelper.ForceCancelDebugMode();
        }

        [Test]
        public void Default_NoCompletedLevels_NoLives()
        {
            Assert.IsFalse(LevelManager.Instance.CanPlayLevel(0));
        }

        [Test]
        public void CompletedLevelOne_NoLives()
        {
            LevelManager.Instance.SetupLevels("Golem", new Level[] { GetLevel(true, 1) });

            Assert.IsFalse(LevelManager.Instance.CanPlayLevel(0));
        }
        
        [Test]
        public void NoCompletedLevels_OneLife()
        {
            LivesManager.Instance.GainALife();

            Assert.IsFalse(LevelManager.Instance.CanPlayLevel(0));
        }

        [Test]
        public void CompletedLevelOne_OneLife()
        {
            LivesManager.Instance.GainALife();
            LevelManager.Instance.SetupLevels("Golem", new Level[] { GetLevel(true, 1) });

            Assert.IsTrue(LevelManager.Instance.CanPlayLevel(0));
        }

        private Level GetLevel(bool completed, int level)
        {
            var lvl = new Level();

            if (completed)
            {
                lvl.LevelProgress = new Scripts.Workers.IO.Data_Entities.LevelProgress();
                lvl.LevelProgress.Level = level;
                lvl.LevelProgress.StarAchieved = 1;
            }
            return lvl;
        }
    }
}
