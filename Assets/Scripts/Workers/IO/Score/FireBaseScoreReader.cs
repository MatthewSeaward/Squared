using Assets.Scripts.Workers.IO.Helpers;
using DataEntities;
using Firebase.Database;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Score
{

    public delegate void ScoresLoaded(List<ScoreEntry> scores);

    class FireBaseScoreReader : IScoreReader
    {
        public static ScoresLoaded ScoresLoaded;

        public Dictionary<int, List<int>> GetScores(string chapter, int level)
        {
            throw new NotImplementedException();
        }

        public void GetScoresAsync(string chapter, int level)
        {
            try
            {
                FireBaseDatabase.Database.Child(FireBaseSavePaths.ScoreLocation(chapter, level + 1))
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
                                          
                                            var result = new List<ScoreEntry>();

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
        }
    }
}
