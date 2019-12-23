using NUnit.Framework;

namespace Assets.Scripts.Editor.Workers.Managers.Level_Manager
{
    [Category("Level Manager")]
    class LevelProgressTests
    {
        [Test]
        public void IsNotNull()
        {
            Assert.IsNotNull(LevelManager.Instance.LevelProgress);
        }

        [Test]
        public void SetToNull_IsNotNul()
        {
            LevelManager.Instance.LevelProgress = null;

            Assert.IsNotNull(LevelManager.Instance.LevelProgress);
        }

        [Test]
        public void AddData()
        {
            LevelManager.Instance.LevelProgress.Add(new Scripts.Workers.IO.Data_Entities.LevelProgress());

            Assert.AreEqual(1, LevelManager.Instance.LevelProgress.Count);
        }
    }
}
