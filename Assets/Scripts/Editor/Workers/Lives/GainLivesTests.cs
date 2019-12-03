using Assets.Scripts.Workers.Data_Managers;
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
            LivesManager.Reset();

            LivesManager.GainALife();

            Assert.AreEqual(1, LivesManager.LivesRemaining);
        }

        [Test]
        public void GainALife_CannotGoAbove_6()
        {
            LivesManager.Reset();
            
            for (int i = 0; i < 7; i++)
            {
                LivesManager.GainALife();
            }

            Assert.AreEqual(6, LivesManager.LivesRemaining);
        }

        [Test]
        public void LoseALife_NewLives_1()
        {            
            LivesManager.Reset();

            LivesManager.GainALife();
            Assert.AreEqual(1, LivesManager.LivesRemaining);

            LivesManager.LoseALife();
            Assert.AreEqual(0, LivesManager.LivesRemaining);
        }

        [Test]
        public void LoseALife_CannotGoBelow_0()
        {
            LivesManager.Reset();

            Assert.AreEqual(0, LivesManager.LivesRemaining);

            LivesManager.LoseALife();
            Assert.AreEqual(0, LivesManager.LivesRemaining);
        }

        [Test]
        public void GainALife_EventThrown()
        {
            LivesManager.Reset();

            bool? livesGained = null;
            int lives = 0;
            LivesManager.LivesChanged += (bool gained, int newLives) =>
            {
                livesGained = gained;
                lives = newLives;
            };

            LivesManager.GainALife();

            Assert.IsTrue(livesGained);
            Assert.AreEqual(1, lives);
        }

        [Test]
        public void LoseALife_EventThrown()
        {
            LivesManager.Reset();

            LivesManager.GainALife();
            Assert.AreEqual(1, LivesManager.LivesRemaining);

            bool? livesGained = null;
            int lives = 0;

            LivesManager.LivesChanged += (bool gained, int newLives) =>
            {
                livesGained = gained;
                lives = newLives;
            };

            LivesManager.LoseALife();

            Assert.IsFalse(livesGained);
            Assert.AreEqual(0, lives);
        }

        [Test]
        public void LoseALife_At0_NoEventThrown()
        {
            LivesManager.Reset();

            Assert.AreEqual(0, LivesManager.LivesRemaining);

            bool? livesGained = null;
            int lives = 0;

            LivesManager.LivesChanged += (bool gained, int newLives) =>
            {
                livesGained = gained;
                lives = newLives;
            };

            LivesManager.LoseALife();

            Assert.IsNull(livesGained);
            Assert.AreEqual(0, lives);
        }
    }
}
