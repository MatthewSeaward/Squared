using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Helpers
{
    public static class FireBaseReader
    {
        public async static Task<List<T>> ReadAsync<T>(string path)
        {
            var result = new List<T>();

            try
            {
                await FireBaseDatabase.Database.Child(path).GetValueAsync().ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                    }
                    else if (task.IsCompleted)
                    {
                        try
                        {
                            DataSnapshot snapshot = task.Result;

                            foreach (var child in snapshot.Children)
                            {
                                foreach (var item in child.Children)
                                {
                                    string info = item?.GetRawJsonValue()?.ToString();

                                    if (info != null)
                                    {
                                        var data = JsonUtility.FromJson<T>(info);
                                        result.Add(data);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                });
            }
            catch { }

            return result;
        }
    }
}
