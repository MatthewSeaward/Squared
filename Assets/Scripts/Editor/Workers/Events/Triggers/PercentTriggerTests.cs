using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Enemy.Events;
using NUnit.Framework;
using System.Collections.Generic;

namespace Assets.Scripts.Editor.Workers.Events.Triggers
{
    [Category("Events")]
    class PercentTriggerTests
    {
        [Test]
        public void OnePiece_OneMatching_100Percent_Triggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceFactory.PieceTypes.Rainbow, 100);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage = enemyEvent;

            PieceController.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Rainbow)
            }, new float[1], new float[1]) ;

            trigger.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }

        [Test]
        public void OnePiece_NoneMatching_0Percent_DoesNotTriggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceFactory.PieceTypes.Rainbow, 0);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage = enemyEvent;

            PieceController.Setup(new List<ISquarePiece>()
            {
                 TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }

        [Test]
        public void TwoPieces_OneMatching_100Percent_DoesNotTriggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceFactory.PieceTypes.Rainbow, 100);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage = enemyEvent;

            PieceController.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsFalse(enemyEvent.Activated);
        }

        [Test]
        public void TwoPieces_OneMatching_50Percent_Triggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceFactory.PieceTypes.Rainbow, 50);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage = enemyEvent;

            PieceController.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }

        [Test]
        public void TenPieces_OneMatching_50Percent_DoesNotTriggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceFactory.PieceTypes.Rainbow, 50);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage = enemyEvent;

            PieceController.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsFalse (enemyEvent.Activated);
        }

        [Test]
        public void TenPieces_ThreeMatching_30Percent_Triggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceFactory.PieceTypes.Rainbow, 30);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage = enemyEvent;

            PieceController.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }

        [Test]
        public void TenPieces_FiveMatching_30Percent_Triggers()
        {
            var trigger = new PiecePercentEventTrigger(PieceFactory.PieceTypes.Rainbow, 30);

            var enemyEvent = new ChangeBoolEvent();
            trigger.EnemyRage = enemyEvent;

            PieceController.Setup(new List<ISquarePiece>()
            {
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Rainbow),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal),
                TestHelpers.CreatePiece(PieceFactory.PieceTypes.Normal)
            }, new float[1], new float[1]);

            trigger.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }

    }
}
