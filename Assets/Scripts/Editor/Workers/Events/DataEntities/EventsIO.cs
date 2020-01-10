using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using NUnit.Framework;
using UnityEngine;

namespace Assets.Scripts.Editor.Workers.Events.DataEntities
{
    [Category("Events")]
    class EventsIO
    {
        [Test]
        public void SimpleEvent_ToJSON_FromJSON()
        {
            var levelEvents = new LevelEvents[1];

            var e = new LevelEvents();
            e.LevelNumber = 1;

            e.Star1 = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType="Destroy", NumberOfPiecesToSelect=2} } },
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="3-4", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType="Destroy", NumberOfPiecesToSelect=2} } } };

            e.Star2 = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="2-5", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Change", TypeOfPieceToSelect = "l" } } },
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="5-7", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType="Destroy", NumberOfPiecesToSelect=4} } } };

            e.Star2 = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="2-5", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Add", PositionsSelected= new string[] {"3:2", "2:4", "3:3" }, NewPieceType ="x"  },
                                                                                                                         new LevelEvents.Event() { EventType="ChangeS", TypeOfPieceToSelect="x", NewPieceType ="2"} } } };

            levelEvents[0] = e;

            var jsonFormat = JsonHelper.ToJson(levelEvents);
            Debug.Log(jsonFormat);

            Assert.IsFalse(string.IsNullOrWhiteSpace(jsonFormat));

            var decodedJson = JsonHelper.FromJson<LevelEvents>(jsonFormat);

            Assert.IsNotNull(decodedJson);
            Assert.AreEqual(1, decodedJson.Length);
            Assert.AreEqual(1, decodedJson[0].LevelNumber);
            Assert.IsNotNull(decodedJson[0].Star1);
            Assert.IsNotNull(decodedJson[0].Star2);
            Assert.IsNotNull(decodedJson[0].Star3);
        }
    }
}				