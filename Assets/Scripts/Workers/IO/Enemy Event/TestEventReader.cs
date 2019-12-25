using System.Collections.Generic;
using Assets.Scripts.Workers.IO.Enemy_Event;
using DataEntities;

namespace Assets.Scripts.Workers.IO
{
    public class TestEventReader : IEventReader
    {
        public Dictionary<string, LevelEvents[]> GetEvents()
        {
            return new Dictionary<string, LevelEvents[]>
            {
                {"Level1", new LevelEvents[0] },
                {"Level2", new LevelEvents[0] }
            };
        }
    }
}
