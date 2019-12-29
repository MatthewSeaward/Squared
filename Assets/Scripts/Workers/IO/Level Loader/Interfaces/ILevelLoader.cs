using Assets.Scripts.Workers.IO.Data_Entities;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.IO.Interfaces
{
    public interface ILevelLoader
    {
        Dictionary<string, Level[]> GetLevels();        
    }
}