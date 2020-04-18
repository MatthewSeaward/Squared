using Assets.Scripts.Workers.Enemy;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.IO.Data_Entities;
using NUnit.Framework;
using System;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;

namespace Assets.Scripts.Editor.Workers.Events.Enemy_Events_Factory
{
    [Category("Events")]
    class EnemyEventFactoryResilenceTests
    {
        [Test]
        public void InvalidEventType()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "abc",  NumberOfPiecesToSelect = 3  } } } };

             Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }

        [Test]
        public void InvalidTrigger()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="abc", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Destroy",  NumberOfPiecesToSelect = 3  } } } };
                       
            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }

        [Test]
        public void InvalidNewPieceType()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Change", NewPieceType = "*" , NumberOfPiecesToSelect = 3  } } } };

            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }

        [Test]
        public void EmptyNewPieceType()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Change", NewPieceType = "" , NumberOfPiecesToSelect = 3  } } } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var rage = sut.RageEvents[0].EnemyRage[0] as ChangeRandomPiece;

            Assert.AreEqual(PieceTypes.Normal, rage.NewPieceType);
        }

        [Test]
        public void EmptySelectPieceType()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "ChangeS", NumberOfPiecesToSelect = 3, NewPieceType = "x", TypeOfPieceToSelect = "" } } } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var rage = sut.RageEvents[0].EnemyRage[0] as ChangeSpecificPiece;

            Assert.AreEqual(PieceTypes.Normal, rage.From);
        }

        [Test]
        public void InvalidSelectPieceType()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "ChangeS", NumberOfPiecesToSelect = 3, NewPieceType = "x", TypeOfPieceToSelect = "*" } } } };

            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }

        [Test]
        public void InvalidTriggerOn_InvalidValues_Turns()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="a-b", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Destroy",  NumberOfPiecesToSelect = 3  } } } };

            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }


        [Test]
        public void InvalidTriggerOn_NoDash_Turns()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1.2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Destroy",  NumberOfPiecesToSelect = 3  } } } };

            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }

        [Test]
        public void InvalidTriggerOn_InvalidValues_Percent()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Percent", TriggerOn="*-10", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Destroy",  NumberOfPiecesToSelect = 3  } } } };

            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }

        [Test]
        public void InvalidTriggerOn_NoDash_Percent()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Percent", TriggerOn="r.10", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Destroy",  NumberOfPiecesToSelect = 3  } } } };

            Assert.Throws<ArgumentException>(() => EnemyEventsFactory.GetLevelEvents(testEvent));
        }
    }
}
