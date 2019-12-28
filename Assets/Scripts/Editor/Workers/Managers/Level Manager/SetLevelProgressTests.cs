using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Level_Info;
using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Scripts.Editor.Workers.Managers.Level_Manager
{
    [Category("Level Manager")]
    class SetLevelProgressTests
    {
        [SetUp]
        public void Setup()
        {
            LevelManager.Instance.Levels.Clear();
        }

        [Test]
        public void SingleLevelProgress()
        {
            var level = new Level() { LevelNumber = 1 };
            var levels = new Dictionary<string, Level[]>();
            levels.Add("Golem", new Level[] { level });

            LevelManager.Instance.SetupLevels(levels, new string[] { "Golem" });

            LevelManager.Instance.SetLevelProgress(new LevelProgress[] { new LevelProgress() { Chapter = "Golem", StarAchieved = 2, Level = 0 } });

            Assert.IsNotNull(level.LevelProgress);
            Assert.AreEqual(2, level.LevelProgress.StarAchieved);
        }

        [Test]
        public void AddProgressForInvalidLevel()
        {
            var levels = new Dictionary<string, Level[]>();
            levels.Add("Golem", new Level[] { new Level() });

            LevelManager.Instance.SetupLevels(levels, new string[] { "Golem" });

            Assert.DoesNotThrow(() =>
                                 LevelManager.Instance.SetLevelProgress(new LevelProgress[] { new LevelProgress() { Chapter = "Golem", StarAchieved = 2, Level = 20 } })
                                );  
        }

        [Test]
        public void AddProgressForInvalidChapter()
        {
            var levels = new Dictionary<string, Level[]>();
            levels.Add("Golem", new Level[] { new Level() });

            LevelManager.Instance.SetupLevels(levels, new string[] { "Golem" });

            Assert.DoesNotThrow(() =>
                                 LevelManager.Instance.SetLevelProgress(new LevelProgress[] { new LevelProgress() { Chapter = "Wolf", StarAchieved = 2, Level = 20 } })
                                );
        }

        [Test]
        public void AddNullProgress()
        {
            var levels = new Dictionary<string, Level[]>();
            levels.Add("Golem", new Level[] { new Level() });

            LevelManager.Instance.SetupLevels(levels, new string[] { "Golem" });

            Assert.DoesNotThrow(() =>
                                 LevelManager.Instance.SetLevelProgress(null)
                                );
        }
    }
}
