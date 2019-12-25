using Assets.Scripts.Workers.IO;
using NUnit.Framework;
using System;

namespace Assets.Scripts.Editor.Workers.IO.Level_IO
{
    [Category("IO")]
    class LevelIOInitialisation
    {
        [SetUp]
        public void Setup()
        {
            LevelIO.Instance.Reset();
        }

        [Test]
        public void Initialised_CallMethod()
        {
            LevelIO.Instance.Initialise(new TestLevelLoader(), new TestProgressReader(), new TestProgressWriter(), new TestLevelOrderLoader(), new TestEventReader());

            Assert.DoesNotThrow(() => LevelIO.Instance.ResetSavedData());
        }

        [Test]
        public void NotInitialised_CallMethod_ExceptionThrown()
        {
            Assert.Throws<InvalidOperationException>(() => LevelIO.Instance.ResetSavedData());
        }
    }
}
