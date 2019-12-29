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

            e.Star1 = new LevelEvents.Event[] {
                new LevelEvents.Event() {  EventType="Destroy", Trigger="Turns", TriggerOn="1-2", NumberOfPiecesToSelect=2},
                new LevelEvents.Event() {  EventType="Destroy", Trigger="Turns", TriggerOn="3-4", NumberOfPiecesToSelect=2} };

            e.Star2 = new LevelEvents.Event[] {
                new LevelEvents.Event() {  EventType="Change", Trigger="Turns", TriggerOn="2-5", TypeOfPieceToSelect="l"},
                new LevelEvents.Event() {  EventType="Destroy", Trigger="Turns", TriggerOn="5-7", NumberOfPiecesToSelect=4} };

            e.Star3 = new LevelEvents.Event[] {
                new LevelEvents.Event() {  EventType="Add", Trigger="Turns", TriggerOn="2-5", PositionsSelected= new string[] {"3:2", "2:4", "3:3" }, NewPieceType ="x" },
                new LevelEvents.Event() {  EventType="ChangeS", Trigger="Turns", TriggerOn="5-7", TypeOfPieceToSelect="x", NewPieceType ="2"} };

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