using Assets.Scripts.Workers.Enemy.Events;
using NUnit.Framework;

namespace Assets.Scripts.Editor.Workers.Events.Triggers
{
    [Category("Events")]
    class TurnEventTriggerTests
    {
        [Test]
        public void TriggersAfterFirstTurn_1TurnPasses_Triggers()
        {
            var sut = new TurnEventTrigger(0, 0);

            var enemyEvent = new ChangeBoolEvent();
            sut.EnemyRage.Add(enemyEvent);
            sut.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }

        [Test]
        public void TriggersAfterThirdtTurn_2TurnPasses_NotTriggered()
        {
            var sut = new TurnEventTrigger(3, 3);

            var enemyEvent = new ChangeBoolEvent();
            sut.EnemyRage.Add(enemyEvent);

            sut.CheckForEvent();
            sut.CheckForEvent();

            Assert.IsFalse(enemyEvent.Activated);
        }

        [Test]
        public void TriggersBetweenThirdAndFifthTurnTurn_5TurnPasses_Triggers()
        {
            var sut = new TurnEventTrigger(3, 5);

            var enemyEvent = new ChangeBoolEvent();
            sut.EnemyRage.Add(enemyEvent);
            sut.CheckForEvent();
            sut.CheckForEvent();
            sut.CheckForEvent();
            sut.CheckForEvent();
            sut.CheckForEvent();

            Assert.IsTrue(enemyEvent.Activated);
        }
    }
}
