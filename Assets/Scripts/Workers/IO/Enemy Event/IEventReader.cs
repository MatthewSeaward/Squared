using DataEntities;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.IO.Enemy_Event
{
    public interface IEventReader
    {
        Dictionary<string, LevelEvents[]> GetEvents(); 
    }
}