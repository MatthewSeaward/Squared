using System.Collections.Generic;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Enemy_Event;

namespace Assets.Scripts.Workers.IO.Scripts.Workers.IO.Data_Entities
{
    public class TestEventReader : IEventReader
    {
        public Dictionary<string, LevelEvents[]> GetEvents()
        {
            var dict = new Dictionary<string, LevelEvents[]>();

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

            dict.Add("Golem", new LevelEvents[] { e });

            return dict;           
        }
    }
}
