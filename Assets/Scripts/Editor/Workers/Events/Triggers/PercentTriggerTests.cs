using Assets.Scripts.Workers.Enemy.Events;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using NUnit.Framework;
using System.Collections.Generic;
using static SquarePiece;

namespace Assets.Scripts.Editor.Workers.Events.Triggers
{
    [Category("Events")]
    class PercentTriggerTests
    {
        [Test]
        public void PercentParse_TypePercent()
        {
            var trigger = new PiecePercentEventTrigger("r-30");
            
            Assert.AreEqual(trigger.Percent, 30);
            Assert.AreEqual(trigger.PieceType, PieceBuilderDirector.PieceTypes.Rainbow);
        }

        [Test]
        public void PercentParse_TypeColour()
        {
            var trigger = new PiecePercentEventTrigger("-30-1");

            Assert.AreEqual(trigger.Percent, 30);
            Assert.AreEqual(trigger.Colour, Colour.Orange);
        }

        [Test]
        public void PercentParse_TypePercentColour()
        {
            var trigger = new PiecePercentEventTrigger("r-30-1");

            Assert.AreEqual(trigger.Percent, 30);
            Assert.AreEqual(trigger.PieceType, PieceBuilderDirector.PieceTypes.Rainbow);
            Assert.AreEqual(trigger.Colour, Colour.Orange);
        }

        [Test]
        public void OnePiece_OneMatching_100Percent_Colour_Triggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceBuilderDirector.PieceTypes.Empty, 100, Colour.Red);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage.Add(enemyEvent);

            PieceManager.Instance.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(0, 0, Colour.Red)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }

        [Test]
        public void TwoPieces_OneMatching_50Percent_Colour_Triggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceBuilderDirector.PieceTypes.Empty, 50, Colour.Red);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage.Add(enemyEvent);

            PieceManager.Instance.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(0, 0, Colour.Red),
                TestHelpers.CreatePiece(0, 1, Colour.DarkBlue),

            }, new float[1], new float[2]);

            trigger.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }

        [Test]
        public void OnePiece_NoneMatching_100Percent_Colour_DoesNotTrigger()
        {
            var trigger = new PiecePercentEventTrigger(PieceBuilderDirector.PieceTypes.Empty, 100, Colour.Red);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage.Add(enemyEvent);

            PieceManager.Instance.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(0, 0, Colour.Green)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsFalse(enemyEvent.Activated);
        }

        [Test]
        public void OnePiece_OneMatching_100Percent_Triggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceBuilderDirector.PieceTypes.Rainbow, 100);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage.Add(enemyEvent);

            PieceManager.Instance.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Rainbow)
            }, new float[1], new float[1]) ;

            trigger.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }

        [Test]
        public void OnePiece_NoneMatching_0Percent_DoesNotTriggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceBuilderDirector.PieceTypes.Rainbow, 0);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage.Add(enemyEvent);

            PieceManager.Instance.Setup(new List<ISquarePiece>()
            {
                 TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }

        [Test]
        public void TwoPieces_OneMatching_100Percent_DoesNotTriggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceBuilderDirector.PieceTypes.Rainbow, 100);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage.Add(enemyEvent);

            PieceManager.Instance.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsFalse(enemyEvent.Activated);
        }

        [Test]
        public void TwoPieces_OneMatching_50Percent_Triggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceBuilderDirector.PieceTypes.Rainbow, 50);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage.Add(enemyEvent);

            PieceManager.Instance.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }

        [Test]
        public void TenPieces_OneMatching_50Percent_DoesNotTriggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceBuilderDirector.PieceTypes.Rainbow, 50);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage.Add(enemyEvent);

            PieceManager.Instance.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsFalse (enemyEvent.Activated);
        }

        [Test]
        public void TenPieces_ThreeMatching_30Percent_Triggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceBuilderDirector.PieceTypes.Rainbow, 30);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage.Add(enemyEvent);

            PieceManager.Instance.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }

        [Test]
        public void TenPieces_FiveMatching_30Percent_Triggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceBuilderDirector.PieceTypes.Rainbow, 30);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage.Add(enemyEvent);

            PieceManager.Instance.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceBuilderDirector.PieceTypes.Normal)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }

    }
}
