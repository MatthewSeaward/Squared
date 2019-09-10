using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Heatmap
{
    public interface IHeatmapWriter
    {
        void WriteHeatmapData(string chapter, int level, Dictionary<Vector2Int, int> usedPieces);
    }
}
