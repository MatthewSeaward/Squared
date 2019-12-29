using Assets.Scripts.Workers.Managers;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Scripts.Editor.Workers.Lives
{
    [Category("Lives")]
    class GainLivesTests
    {
        [Test]
        public void GainALife_NewLives_1()
        {
            LivesManager.Instance.Reset();

            LivesManager.Instance.GainALife();

            Assert.AreEqual(1, LivesManager.Instance.LivesRemaining);
        }

        [Test]
        public void GainALife_CannotGoAbove_6()
        {
            LivesManager.Instance.Reset();
            
            for (int i = 0; i < 7; i++)
            {
                LivesManager.Instance.GainALife();
            }

            Assert.AreEqual(6, LivesManager.Instance.LivesRemaining);
        }

        [Test]
        public void LoseALife_NewLives_1()
        {            
            LivesManager.Instance.Reset();

            LivesManager.Instance.GainALife();
            Assert.AreEqual(1, LivesManager.Instance.LivesRemaining);

            LivesManager.Instance.LoseALife();
            Assert.AreEqual(0, LivesManager.Instance.LivesRemaining);
        }

        [Test]
        public void LoseALife_CannotGoBelow_0()
        {
            LivesManager.Instance.Reset();

            Assert.AreEqual(0, LivesManager.Instance.LivesRemaining);

            LivesManager.Instance.LoseALife();
            Assert.AreEqual(0, LivesManager.Instance.LivesRemaining);
        }

        [Test]
        public void GainALife_EventThrown()
        {
            LivesManager.Instance.Reset();

            bool? livesGained = null;
            int lives = 0;
            LivesManager.LivesChanged += (bool gained, int newLives) =>
            {
                livesGained = gained;
                lives = newLives;
            };

            LivesManager.Instance.GainALife();

            Assert.IsTrue(livesGained);
            Assert.AreEqual(1, lives);
        }

        [Test]
        public void LoseALife_EventThrown()
        {
            LivesManager.Instance.Reset();

            LivesManager.Instance.GainALife();
            Assert.AreEqual(1, LivesManager.Instance.LivesRemaining);

            bool? livesGained = null;
            int lives = 0;

            LivesManager.LivesChanged += (bool gained, int newLives) =>
            {
                livesGained = gained;
                lives = newLives;
            };

            LivesManager.Instance.LoseALife();

            Assert.IsFalse(livesGained);
            Assert.AreEqual(0, lives);
        }

        [Test]
        public void LoseALife_At0_NoEventThrown()
        {
            LivesManager.Instance.Reset();

            Assert.AreEqual(0, LivesManager.Instance.LivesRemaining);

            bool? livesGained = null;
            int lives = 0;

            LivesManager.LivesChanged += (bool gained, int newLives) =>
            {
                livesGained = gained;
                lives = newLives;
            };

            LivesManager.Instance.LoseALife();

            Assert.IsNull(livesGained);
            Assert.AreEqual(0, lives);
        }
    }
}
