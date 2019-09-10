using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Heatmap
{
    public delegate void HeatmapLoaded(Dictionary<string, int> results);

    class FireBaseHeatmapReader : IHeatMapReader
    {
        public static HeatmapLoaded HeatmapLoaded;

        DatabaseReference Database = null;

        public Dictionary<string, int> GetHeatmap(string chapter, int level)
        {           
            GetDatabase();

            var result = new Dictionary<string, int>();

            try
            {
                FirebaseDatabase.DefaultInstance
                             .GetReference($"HeatMap/{chapter}/{level}")
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

                                         var list =  snapshot.Value as List<object>;
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

            return result;
        }
 

        private void GetDatabase()
        {
            if (Database != null)
            {
                return;
            }
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://squared-105cf.firebaseio.com/");

            // Get the root reference location of the database.
            Database = FirebaseDatabase.DefaultInstance.RootReference;

        }
    }
}
