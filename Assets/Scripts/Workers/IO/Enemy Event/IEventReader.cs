using Assets.Scripts.Workers.Enemy.Events;
using DataEntities;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.IO.Enemy_Event
{
    interface IEventReader
    {
        Dictionary<string, LevelEvents[]> GetEvents(); 
    }
}
