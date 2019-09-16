using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Heatmap
{
    class DebugHeatmapReader : IHeatMapReader
    {
        public Dictionary<string, int> GetHeatmap(string chapter, int level)
        {
            return new Dictionary<string, int>
            {
                { "0,1", 2 },
                { "0,5", 5 },
                { "0,6", 1 },
                { "1,2", 4 },
                { "1,3", 8 },
                { "1,6", 4 },
                { "2,0", 2 },
                { "2,1", 2 },
                { "2,5", 5 },
                { "3,0", 3 },
                { "3,6", 8 },
                { "3,9", 2 },
            };
        }

        public void GetHeatmapAsync(string chapter, int level)
        {
            throw new System.NotImplementedException();
        }
    }
}
