using Assets.Scripts.Workers.IO.Data_Entities;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.IO
{
    public interface IBarkLoader
    {
        Dictionary<string, Barks> GetBarks();
    }
}
