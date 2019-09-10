using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Heatmap
{
    public class HeatMapWriter
    {
        private IHeatmapWriter _dataWriter = new FirebaseHeatMapWriter();

        private static HeatMapWriter _instance;

        public static HeatMapWriter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HeatMapWriter();
                }

                return _instance;
            }
        }

        private HeatMapWriter() { }

        private void ScoreKeeper_GameCompleted(string chapter, int level, Dictionary<Vector2Int, int> usedPieces)
        {
            _dataWriter.WriteHeatmapData(chapter, level, usedPieces);
        }
    }
}
