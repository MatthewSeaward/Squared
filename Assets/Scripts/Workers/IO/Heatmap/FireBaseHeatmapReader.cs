using Assets.Scripts.Workers.IO.Helpers;
using Firebase.Database;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Heatmap
{
    public delegate void HeatmapLoaded(Dictionary<string, int> results);

    class FireBaseHeatmapReader : IHeatMapReader
    {
        public static HeatmapLoaded HeatmapLoaded;
        
        public Dictionary<string, int> GetHeatmap(string chapter, int level)
        {
            throw new NotImplementedException();
        }

        public void GetHeatmapAsync(string chapter, int level)
        {
            try
            {
                FireBaseDatabase.Database.Child(FireBaseSavePaths.HeatMapLocation(chapter, level))
                             .GetValueAsync().ContinueWith(task =>
                             {
                                 if (task.IsFaulted)
                                 {
                                 }
                                 else if (task.IsCompleted)
                                 {
                                     try
                                     {
                                         DataSnapshot snapshot = task.Result;
                                         var result = new Dictionary<string, int>();

                                         var list = snapshot.Value as List<object>;
                                         foreach (var item in list)
                                         {
                                             var parsedChild = item as Dictionary<string, object>;
                                             
                                             var key = parsedChild["Key"].ToString();
                                             var uses = parsedChild["Uses"].ToString();

                                             int childValue = int.Parse(uses);

                                             result.Add(key, childValue);
                                         }

                                         HeatmapLoaded?.Invoke(result);
                                     }
                                     catch (Exception ex)
                                     {
                                         Debug.LogError(ex);
                                     }
                                 }
                             });
            }
            catch { }
        }
      
    }
}
