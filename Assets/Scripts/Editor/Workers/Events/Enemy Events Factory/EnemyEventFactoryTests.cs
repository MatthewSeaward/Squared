using Assets.Scripts.Workers.Enemy;
using Assets.Scripts.Workers.Enemy.Events;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.IO.Data_Entities;
using NUnit.Framework;
using UnityEngine;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;
using static SquarePiece;

namespace Assets.Scripts.Editor.Workers.Events.Enemy_Events_Factory
{
    [Category("Events")]
    class EnemyEventFactoryTests
    {
        [Test]
        public void TurnTrigger_DestroyEvent()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Destroy",  NumberOfPiecesToSelect = 3  } } } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as TurnEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage[0] as DestroyRage;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(3, rage.SelectionAmount);
            Assert.AreEqual(1, eventType.TurnRange.Min);
            Assert.AreEqual(2, eventType.TurnRange.Max);
        }

        [Test]
        public void TurnTrigger_SwapEvent()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Swap",  NumberOfPiecesToSelect = 3  } } } };


            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as TurnEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage[0] as SwapRage;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(3, rage.SelectionAmount);
            Assert.AreEqual(1, eventType.TurnRange.Min);
            Assert.AreEqual(2, eventType.TurnRange.Max);
        }

        [Test]
        public void TurnTrigger_ChangeEvent()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Change",  NumberOfPiecesToSelect = 3, NewPieceType = "r" } } } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as TurnEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage[0] as ChangeRandomPiece;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(3, rage.SelectionAmount);
            Assert.AreEqual(PieceTypes.Rainbow, rage.NewPieceType);
            Assert.AreEqual(1, eventType.TurnRange.Min);
            Assert.AreEqual(2, eventType.TurnRange.Max);
        }

        [Test]
        public void TurnTrigger_ChangeColourEvent()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "ChangeC",  NumberOfPiecesToSelect = 3, NewPieceType = "0", TypeOfPieceToSelect = "1" } } } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as TurnEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage[0] as ChangeColourEvent;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(3, rage.SelectionAmount);
            Assert.AreEqual(Colour.Orange, rage.From);
            Assert.AreEqual(Colour.Red, rage.To);
            Assert.AreEqual(1, eventType.TurnRange.Min);
            Assert.AreEqual(2, eventType.TurnRange.Max);
        }

        [Test]
        public void TurnTrigger_AddColourEvent()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "AddC",  NewPieceType = "0" } } } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as TurnEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage[0] as AddSpriteEvent;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(Colour.Red, rage.NewColour);
            Assert.AreEqual(1, eventType.TurnRange.Min);
            Assert.AreEqual(2, eventType.TurnRange.Max);
        }


        [Test]
        public void TurnTrigger_RemoveColourEvent()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "RemoveC",  NewPieceType = "0" } } } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as TurnEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage[0] as RemoveSpriteEvent;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(Colour.Red, rage.ColourToRemove);
            Assert.AreEqual(1, eventType.TurnRange.Min);
            Assert.AreEqual(2, eventType.TurnRange.Max);
        }

        [Test]
        public void PercentTrigger_ChangeSpecificPieceEvent()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Percent", TriggerOn="r-30", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "ChangeS", TypeOfPieceToSelect = "x", NumberOfPiecesToSelect = 2, NewPieceType = "r" } } } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as PiecePercentEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage[0] as ChangeSpecificPiece;

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
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Percent", TriggerOn="r-30", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Destroy",  NumberOfPiecesToSelect = 2 } } } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as PiecePercentEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage[0] as DestroyRage;

            Assert.NotNull(eventType);
            Assert.NotNull(rage);

            Assert.AreEqual(2, rage.SelectionAmount);
            Assert.AreEqual(30, eventType.Percent);
            Assert.AreEqual(PieceTypes.Rainbow, eventType.PieceType);
        }

        [Test]
        public void PercentTrigger_AddEvent()
        {
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Percent", TriggerOn="r-30", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Add", NewPieceType = "r", PositionsSelected = new string[] { "1:2", "2:3", "3:4", "4:5" } } } } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as PiecePercentEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage[0] as AddPieceEvent;

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
            var testEvent = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Percent", TriggerOn="r-30", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Remove", PositionsSelected = new string[] { "1:2", "2:3", "3:4", "4:5" } } } } };

            var sut = EnemyEventsFactory.GetLevelEvents(testEvent);

            var eventType = sut.RageEvents[0] as PiecePercentEventTrigger;
            var rage = sut.RageEvents[0].EnemyRage[0] as RemovePieceEvent;

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