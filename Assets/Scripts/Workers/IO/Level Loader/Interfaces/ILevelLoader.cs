using DataEntities;
using System.Collections.Generic;

namespace LevelLoader.Interfaces
{
    interface ILevelLoader
    {
        Dictionary<string, Level[]> GetLevels();
        
    }
}