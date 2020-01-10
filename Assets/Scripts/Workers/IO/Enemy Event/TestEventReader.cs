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

            e.Star1 = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="1-2", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType="Destroy", NumberOfPiecesToSelect=2} } },
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="3-4", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType="Destroy", NumberOfPiecesToSelect=2} } } };

            e.Star2 = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="2-5", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Change", TypeOfPieceToSelect = "l" } } },
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="5-7", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType="Destroy", NumberOfPiecesToSelect=4} } } };

            e.Star2 = new LevelEvents.EventTrigger[] {
                new LevelEvents.EventTrigger() { Trigger="Turns", TriggerOn="2-5", Events =  new LevelEvents.Event[] { new LevelEvents.Event() { EventType= "Add", PositionsSelected= new string[] {"3:2", "2:4", "3:3" }, NewPieceType ="x"  },
                                                                                                                         new LevelEvents.Event() { EventType="ChangeS", TypeOfPieceToSelect="x", NewPieceType ="2"} } } };
            dict.Add("Golem", new LevelEvents[] { e });

            return dict;           
        }
    }
}
