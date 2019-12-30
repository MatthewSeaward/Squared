using Assets.Scripts.Workers.Enemy;
using Assets.Scripts.Workers.Enemy.Events;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.IO.Data_Entities;
using NUnit.Framework;
using UnityEngine;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;

namespace Assets.Scripts.Editor.Workers.Events.Enemy_Events_Factory
{
    [Category("Events")]
    class EnemyEventFactoryTests
    {
        [Test]
        public void TurnTrigger_DestroyEvent()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "Destroy", Trigger = "Turns", TriggerOn = "1-2", NumberOfPiecesToSelect = 3 } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as TurnEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage as DestroyRage;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(3, rage.SelectionAmount);
            Assert.AreEqual(1, eventType.TurnRange.Min);
            Assert.AreEqual(2, eventType.TurnRange.Max);
        }

        [Test]
        public void TurnTrigger_SwapEvent()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "Swap", Trigger = "Turns", TriggerOn = "1-2", NumberOfPiecesToSelect = 3 } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as TurnEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage as SwapRage;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(3, rage.SelectionAmount);
            Assert.AreEqual(1, eventType.TurnRange.Min);
            Assert.AreEqual(2, eventType.TurnRange.Max);
        }

        [Test]
        public void TurnTrigger_ChangeEvent()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "Change", Trigger = "Turns", TriggerOn = "1-2", NumberOfPiecesToSelect = 3, NewPieceType = "r" } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as TurnEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage as ChangeRandomPiece;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(3, rage.SelectionAmount);
            Assert.AreEqual(PieceTypes.Rainbow, rage.NewPieceType);
            Assert.AreEqual(1, eventType.TurnRange.Min);
            Assert.AreEqual(2, eventType.TurnRange.Max);
        }

        [Test]
        public void PercentTrigger_ChangeSpecificPieceEvent()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "ChangeS", Trigger = "Percent", TriggerOn = "r-30", NumberOfPiecesToSelect = 2, NewPieceType="r", TypeOfPieceToSelect="x" } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as PiecePercentEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage as ChangeSpecificPiece;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(2, rage.SelectionAmount);
            Assert.AreEqual(PieceTypes.Rainbow, rage.To);
            Assert.AreEqual(PieceTypes.Normal, rage.From);

            Assert.AreEqual(30, eventType.Percent);
            Assert.AreEqual(PieceTypes.Rainbow, eventType.PieceType);
        }

        [Test]
        public void PercentTrigger_DestroyEvent()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "Destroy", Trigger = "Percent", TriggerOn = "r-30", NumberOfPiecesToSelect = 2 } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as PiecePercentEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage as DestroyRage;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(2, rage.SelectionAmount);
            Assert.AreEqual(30, eventType.Percent);
            Assert.AreEqual(PieceTypes.Rainbow, eventType.PieceType);
        }

        [Test]
        public void PercentTrigger_AddEvent()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "Add", Trigger = "Percent", TriggerOn = "r-30", NewPieceType="r", PositionsSelected = new string[] { "1:2", "2:3", "3:4", "4:5" } } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as PiecePercentEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage as AddPieceEvent;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(PieceTypes.Rainbow, rage.Type);
            Assert.AreEqual(4, rage.Positions.Count);
            Assert.AreEqual(new Vector2Int(1, 2), rage.Positions[0]);
            Assert.AreEqual(new Vector2Int(2, 3), rage.Positions[1]);
            Assert.AreEqual(new Vector2Int(3, 4), rage.Positions[2]);
            Assert.AreEqual(new Vector2Int(4, 5), rage.Positions[3]);

            Assert.AreEqual(30, eventType.Percent);
            Assert.AreEqual(PieceTypes.Rainbow, eventType.PieceType);
        }

        [Test]
        public void PercentTrigger_RemovePieceEvent()
        {
            var testEvent = new LevelEvents.Event[] { new LevelEvents.Event { EventType = "Remove", Trigger = "Percent", TriggerOn = "r-30", PositionsSelected = new string[] { "1:2", "2:3", "3:4", "4:5" } } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as PiecePercentEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage as RemovePieceEvent;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(4, rage.Positions.Count);
            Assert.AreEqual(new Vector2Int(1, 2), rage.Positions[0]);
            Assert.AreEqual(new Vector2Int(2, 3), rage.Positions[1]);
            Assert.AreEqual(new Vector2Int(3, 4), rage.Positions[2]);
            Assert.AreEqual(new Vector2Int(4, 5), rage.Positions[3]);

            Assert.AreEqual(30, eventType.Percent);
            Assert.AreEqual(PieceTypes.Rainbow, eventType.PieceType);
        }
    }
}
