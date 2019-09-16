using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Heatmap
{
    interface IHeatMapReader
    {
        Dictionary<string, int> GetHeatmap(string chapter, int level);
        void GetHeatmapAsync(string chapter, int level);
    }
}
