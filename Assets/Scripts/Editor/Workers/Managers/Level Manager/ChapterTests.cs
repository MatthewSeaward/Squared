using DataEntities;
using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Scripts.Editor.Workers.Managers.Level_Manager
{
    [Category("Level Manager")]
    class ChapterTests
    {
        [SetUp]
        public void Setup()
        {
            LevelManager.Instance.Levels.Clear();
            LevelManager.Instance.SetupLevels(new Dictionary<string, Level[]>(),  new string[] { "Golem", "Wolf", "Elf" });
        }        
       
        [Test]
        public void SetChapter_ToValid()
        {
            LevelManager.Instance.SetChapter("Wolf");

            Assert.AreEqual("Wolf", LevelManager.Instance.SelectedChapter);
        }

        [Test]
        public void SetChapter_ToInvalid()
        {
            LevelManager.Instance.SetChapter("Wolf");
            LevelManager.Instance.SetChapter("Blah Blah");

            Assert.AreEqual("Wolf", LevelManager.Instance.SelectedChapter);
        }

        [Test]
        public void ChangeChapter_Next()
        {
            LevelManager.Instance.SetChapter("Wolf");
            Assert.AreEqual("Wolf", LevelManager.Instance.SelectedChapter);

            LevelManager.Instance.NextChapter();
            Assert.AreEqual("Elf", LevelManager.Instance.SelectedChapter);

            LevelManager.Instance.NextChapter();
            Assert.AreEqual("Elf", LevelManager.Instance.SelectedChapter);
        }

        [Test]
        public void ChangeChapter_Previous()
        {
            LevelManager.Instance.SetChapter("Wolf");
            Assert.AreEqual("Wolf", LevelManager.Instance.SelectedChapter);

            LevelManager.Instance.PreviousChapter();
            Assert.AreEqual("Golem", LevelManager.Instance.SelectedChapter);

            LevelManager.Instance.PreviousChapter();
            Assert.AreEqual("Golem", LevelManager.Instance.SelectedChapter);
        }

        [Test]
        public void IsNextChapter()
        {
            LevelManager.Instance.SetChapter("Wolf");

            Assert.IsTrue(LevelManager.Instance.IsNextChapter());

            LevelManager.Instance.NextChapter();
            Assert.IsFalse(LevelManager.Instance.IsNextChapter());
        }

        [Test]
        public void IsPreviousChapter()
        {
            LevelManager.Instance.SetChapter("Wolf");

            Assert.IsTrue(LevelManager.Instance.IsPreviousChapter());

            LevelManager.Instance.PreviousChapter();
            Assert.IsFalse(LevelManager.Instance.IsPreviousChapter());
        }
    }
}
