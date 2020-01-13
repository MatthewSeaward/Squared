using Assets.Scripts.Managers;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Scripts.Editor.Workers.Managers.Game_Manager
{
    [Category("Manager")]
    public class PauseState
    {
        GameManager gameManager;

        [SetUp]
        public void Setup()
        {
            var go = new GameObject();
            gameManager = go.AddComponent<GameManager>();
        }

        [Test]
        public void PauseGame()
        {
            gameManager.ChangePauseState(new object(), true);

            Assert.IsTrue(gameManager.GamePaused);
        }

        [Test]
        public void PauseGame_ThenUnpause()
        {
            var locker = new object();

            gameManager.ChangePauseState(locker, true);
            gameManager.ChangePauseState(locker, false);

            Assert.IsFalse(gameManager.GamePaused);
        }

        [Test]
        public void PauseGame_OtherLockTryToUnPause()
        {
            var locker = new object();
            var otherLocker = new object();

            gameManager.ChangePauseState(locker, true);
            gameManager.ChangePauseState(otherLocker, false);

            Assert.IsTrue(gameManager.GamePaused);
        }


        [Test]
        public void PauseGame_TwoLockers_1()
        {
            var locker = new object();
            var otherLocker = new object();

            gameManager.ChangePauseState(locker, true);

            gameManager.ChangePauseState(otherLocker, true);
            gameManager.ChangePauseState(otherLocker, false);

            Assert.IsTrue(gameManager.GamePaused);
        }

        [Test]
        public void PauseGame_TwoLockers_2()
        {
            var locker = new object();
            var otherLocker = new object();

            gameManager.ChangePauseState(locker, true);

            gameManager.ChangePauseState(otherLocker, true);
            gameManager.ChangePauseState(locker, false);

            Assert.IsTrue(gameManager.GamePaused);
        }

        [Test]
        public void PauseGame_TwoLockers_BothUnpause()
        {
            var locker = new object();
            var otherLocker = new object();

            gameManager.ChangePauseState(locker, true);
            gameManager.ChangePauseState(otherLocker, true);

            gameManager.ChangePauseState(locker, false);
            gameManager.ChangePauseState(otherLocker, false);

            Assert.IsFalse(gameManager.GamePaused);
        }

        [Test]
        public void PauseGame_EventRaised()
        {
            bool eventRaised = false;

            GameManager.PauseStateChanged = (b) => eventRaised = true;

            gameManager.ChangePauseState(new object(), true);

            Assert.IsTrue(eventRaised);
        }

        [Test]
        public void PauseGame_AlreadyPaused_NoEvent()
        {
            bool eventRaised = false;

            gameManager.ChangePauseState(new object(), true);

            GameManager.PauseStateChanged = (b) => eventRaised = true;

            gameManager.ChangePauseState(new object(), true);

            Assert.IsFalse(eventRaised);
        }

        [Test]
        public void UnpauseGame_EventRaised()
        {
            bool eventRaised = false;

            var locker = new object();
            gameManager.ChangePauseState(locker, true);

            GameManager.PauseStateChanged = (b) => eventRaised = true;

            gameManager.ChangePauseState(locker, false);

            Assert.IsTrue(eventRaised);
        }
    }
}
