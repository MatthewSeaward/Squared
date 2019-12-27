using Assets.Scripts.Workers.IO.Data_Entities;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.IO.Enemy_Event
{
    public interface IEventReader
    {
        Dictionary<string, LevelEvents[]> GetEvents(); 
    }
}