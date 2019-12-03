using Assets.Scripts.Workers.Data_Managers;
using NUnit.Framework;
using System;

namespace Assets.Scripts.Editor.Workers.Lives
{
    [Category("Lives")]
    class LivesEarnedAfterTimeTests
    {
        [Test]
        public void FiveMinutesPast_NoLivesGained()
        {
            LivesManager.Reset();

            LivesManager.WorkOutLivesEarned(DateTime.Now.AddMinutes(-5));
            Assert.AreEqual(0, LivesManager.LivesRemaining);
        }

        [Test]
        public void TenMinutesPast_OneLifeGained()
        {
            LivesManager.Reset();

            LivesManager.WorkOutLivesEarned(DateTime.Now.AddMinutes(-10));
            Assert.AreEqual(1, LivesManager.LivesRemaining);
        }

        [Test]
        public void FifteenMinutesPast_OneLifeGained()
        {
            LivesManager.Reset();

            LivesManager.WorkOutLivesEarned(DateTime.Now.AddMinutes(-15));
            Assert.AreEqual(1, LivesManager.LivesRemaining);
        }

        [Test]
        public void ThirtyMinutesPast_ThreeLivesGained()
        {
            LivesManager.Reset();

            LivesManager.WorkOutLivesEarned(DateTime.Now.AddMinutes(-30));
            Assert.AreEqual(3, LivesManager.LivesRemaining);
        }

        [Test]
        public void NinetyMinutesPast_NineLivesGained_CapsAtSix()
        {
            LivesManager.Reset();

            LivesManager.WorkOutLivesEarned(DateTime.Now.AddMinutes(-90));
            Assert.AreEqual(6, LivesManager.LivesRemaining);
        }

        [Test]
        public void NegativeTenMinutesPast_NoLivesGained()
        {
            LivesManager.Reset();

            LivesManager.WorkOutLivesEarned(DateTime.Now.AddMinutes(10));
            Assert.AreEqual(0, LivesManager.LivesRemaining);
        }
    }
}
