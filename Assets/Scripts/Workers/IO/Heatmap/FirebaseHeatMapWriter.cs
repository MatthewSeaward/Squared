using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Heatmap
{
    class FirebaseHeatMapWriter : IHeatmapWriter
    {

        private DatabaseReference Database;

        public void WriteHeatmapData(string chapter, int level, Dictionary<Vector2Int, int> usedPieces)
        {
            GetDatabase();

            Database = FirebaseDatabase.DefaultInstance.GetReference($"HeatMap/{chapter}/{level}");

            foreach (var item in usedPieces)
            {            
                UpdateValue(item.Key, item.Value);
            }
        }

        private void UpdateValue(Vector2Int pos, int used)
        {
            string key = $"{pos.x}:{pos.y}";

            Database.RunTransaction(mutableData =>
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

        private void GetDatabase()
        {
            if (Database != null)
            {
                return;
            }
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://squared-105cf.firebaseio.com/");
        }
    }
}
