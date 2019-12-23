using NUnit.Framework;

namespace Assets.Scripts.Editor.Workers.Managers.Level_Manager
{
    [Category("Level Manager")]
    class GetNextLevelTests
    {
        [SetUp]
        public void Setup()
        {
            LevelManager.Instance.Levels.Clear();
        }

        [Test]
        public void GetCurrentLevel()
        {
            LevelManager.Instance.SetupLevels("Golem", new DataEntities.Level[] { GetLevel(1), GetLevel(2) });

            Assert.AreEqual(0, LevelManager.Instance.CurrentLevel);
        }

        [Test]
        public void GetFirstLevel()
        {
            LevelManager.Instance.SetupLevels("Golem", new DataEntities.Level[] { GetLevel(1), GetLevel(2) });

            var lvl = LevelManager.Instance.GetNextLevel();

            Assert.AreEqual(1, lvl.LevelNumber);
        }
        
        [Test]
        public void GetLevel_DoesNotExist()
        {
            LevelManager.Instance.SetupLevels("Golem", new DataEntities.Level[] { GetLevel(1) });

            LevelManager.Instance.CurrentLevel++;

            var lvl = LevelManager.Instance.GetNextLevel();

            Assert.AreEqual(1, lvl.LevelNumber);
        }

        private DataEntities.Level GetLevel(int number)
        {
            var lvl = new DataEntities.Level();

            lvl.LevelProgress = new Scripts.Workers.IO.Data_Entities.LevelProgress();
            lvl.LevelNumber = number;

            return lvl;
        }
    }
}
