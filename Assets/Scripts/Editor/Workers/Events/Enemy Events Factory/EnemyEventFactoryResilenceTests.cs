using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Enemy;
using Assets.Scripts.Workers.IO.Data_Entities;
using NUnit.Framework;
using System;
using static PieceBuilderDirector;

namespace Assets.Scripts.Editor.Workers.Events.Enemy_Events_Factory
{
    [Category("Events")]
    class EnemyEventFactoryResilenceTests
    {
        [Test]
        public void InvalidEventType()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "abc", Trigger = "Turns", TriggerOn = "1-2", NumberOfPiecesToSelect = 3 } };

             Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }

        [Test]
        public void InvalidTrigger()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "Swap", Trigger = "abc", TriggerOn = "1-2", NumberOfPiecesToSelect = 3 } };

            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }

        [Test]
        public void InvalidNewPieceType()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "Change", Trigger = "Turns", TriggerOn = "1-2", NumberOfPiecesToSelect = 3, NewPieceType = "*" } };

            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }

        [Test]
        public void EmptyNewPieceType()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "Change", Trigger = "Turns", TriggerOn = "1-2", NumberOfPiecesToSelect = 3, NewPieceType = "" } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var rage = sut.RageEvents[0].EnemyRage as ChangeRandomPiece;

            Assert.AreEqual(PieceTypes.Normal, rage.NewPieceType);
        }

        [Test]
        public void EmptySelectPieceType()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "ChangeS", Trigger = "Turns", TriggerOn = "1-2", NumberOfPiecesToSelect = 3, NewPieceType = "x", TypeOfPieceToSelect="" } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var rage = sut.RageEvents[0].EnemyRage as ChangeSpecificPiece;

            Assert.AreEqual(PieceTypes.Normal, rage.From);
        }

        [Test]
        public void InvalidSelectPieceType()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "ChangeS", Trigger = "Turns", TriggerOn = "1-2", NumberOfPiecesToSelect = 3, NewPieceType = "x", TypeOfPieceToSelect="*" } };

            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }

        [Test]
        public void InvalidTriggerOn_InvalidValues_Turns()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "Destroy", Trigger = "Turns", TriggerOn = "a-b", NumberOfPiecesToSelect = 3 } };

            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }


        [Test]
        public void InvalidTriggerOn_NoDash_Turns()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "Destroy", Trigger = "Turns", TriggerOn = "1.2", NumberOfPiecesToSelect = 3 } };

            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }

        [Test]
        public void InvalidTriggerOn_InvalidValues_Percent()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "Destroy", Trigger = "Percent", TriggerOn = "*-10", NumberOfPiecesToSelect = 3 } };

            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }


        [Test]
        public void InvalidTriggerOn_NoDash_Percent()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "Destroy", Trigger = "Percent", TriggerOn = "r.10", NumberOfPiecesToSelect = 3 } };

            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }
    }
}
