using Assets.Scripts.Workers.Managers;
using NUnit.Framework;
using System;

namespace Assets.Scripts.Editor.Workers.Lives
{
    [Category("Lives")]
    class LivesEarnedAfterTimeTests
    {
        [Test]
        public void FourMinutesPast_NoLivesGained()
        {
            LivesManager.Instance.Reset();

            LivesManager.Instance.WorkOutLivesEarned(DateTime.Now.AddMinutes(-4));
            Assert.AreEqual(0, LivesManager.Instance.LivesRemaining);
        }

        [Test]
        public void FourteenMinutesPast_TwoLiveeGained()
        {
            LivesManager.Instance.Reset();

            LivesManager.Instance.WorkOutLivesEarned(DateTime.Now.AddMinutes(-14));
            Assert.AreEqual(2, LivesManager.Instance.LivesRemaining);
        }

        [Test]
        public void SeventeenMinutesPast_ThreeLiveGained()
        {
            LivesManager.Instance.Reset();

            LivesManager.Instance.WorkOutLivesEarned(DateTime.Now.AddMinutes(-17));
            Assert.AreEqual(3, LivesManager.Instance.LivesRemaining);
        }

        [Test]
        public void TwentyMinutesPast_FourLivesGained()
        {
            LivesManager.Instance.Reset();

            LivesManager.Instance.WorkOutLivesEarned(DateTime.Now.AddMinutes(-20));
            Assert.AreEqual(4, LivesManager.Instance.LivesRemaining);
        }

        [Test]
        public void NinetyMinutesPast_NineLivesGained_CapsAtSix()
        {
            LivesManager.Instance.Reset();

            LivesManager.Instance.WorkOutLivesEarned(DateTime.Now.AddMinutes(-90));
            Assert.AreEqual(6, LivesManager.Instance.LivesRemaining);
        }

        [Test]
        public void NegativeTenMinutesPast_NoLivesGained()
        {
            LivesManager.Instance.Reset();

            LivesManager.Instance.WorkOutLivesEarned(DateTime.Now.AddMinutes(10));
            Assert.AreEqual(0, LivesManager.Instance.LivesRemaining);
        }
    }
}
