using Assets.Scripts.Workers.IO.Data_Entities;
using DataEntities;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Score
{

    public delegate void ScoresLoaded(List<ScoreEntry> scores);

    class FireBaseScoreReader : IScoreReader
    {

        DatabaseReference Database = null;
        public static ScoresLoaded ScoresLoaded;

        public Dictionary<int, List<int>> GetScores(string chapter, int level)
        {
            GetDatabase();

            var result = new List<ScoreEntry>();
            try
            {
                FirebaseDatabase.DefaultInstance
                                .GetReference($"Scores/{chapter}/LVL {level + 1}")
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
                                          

                                            foreach(var child in snapshot.Children)
                                            {
                                               
                                                foreach (var item in child.Children)
                                                {
                                                    string info = item?.GetRawJsonValue()?.ToString();
                                                    if (info != null)
                                                    {
                                                        var data = JsonUtility.FromJson<ScoreEntry>(info);
                                                        result.Add(data);
                                                    }
                                                }
                                            }

                                            ScoresLoaded?.Invoke(result);
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                    }
                                });
            }
            catch { }

            return new Dictionary<int, List<int>>();
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
