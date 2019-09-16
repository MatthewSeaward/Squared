using Assets.Scripts.Workers.IO.Helpers;
using Firebase.Database;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Heatmap
{
    class FirebaseHeatMapWriter : IHeatmapWriter
    {

        public void WriteHeatmapData(string chapter, int level, Dictionary<Vector2Int, int> usedPieces)
        {
            foreach (var item in usedPieces)
            {            
                UpdateValue(chapter, level, item.Key, item.Value);
            }
        }

        private void UpdateValue(string chapter, int level, Vector2Int pos, int used)
        {
            string key = $"{pos.x}:{pos.y}";

            FireBaseDatabase.Database.Child($"HeatMap/{chapter}/{level}").RunTransaction(mutableData =>
            {
                List<object> storedData = mutableData.Value as List<object>;

                if (storedData == null)
                {
                    storedData = new List<object>();
                }

                int newUses = used;

                foreach (var child in storedData)
                {
                    var parsedChild = child as Dictionary<string, object>;
                    if (parsedChild == null) { continue; }

                    if (!parsedChild.ContainsKey("Key")) { continue; }
                    if (parsedChild["Key"].ToString() != key) { continue; }

                    if (!parsedChild.ContainsKey("Uses")) { continue; }
                    string data = parsedChild["Uses"].ToString();

                    int childValue = int.Parse(data);
                    newUses += childValue;

                    storedData.Remove(child);

                    break;
                }

                Dictionary<string, object> newMap = new Dictionary<string, object>();
                newMap["Key"] = key;
                newMap["Uses"] = newUses;
                storedData.Add(newMap);
                mutableData.Value = storedData;
                return TransactionResult.Success(mutableData);
            }

            );
        }

    }
}
