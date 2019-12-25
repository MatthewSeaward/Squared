using DataEntities;
using System.Collections.Generic;

namespace LevelLoader.Interfaces
{
    public interface ILevelLoader
    {
        Dictionary<string, Level[]> GetLevels();        
    }
}