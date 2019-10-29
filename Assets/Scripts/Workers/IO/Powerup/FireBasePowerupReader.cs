using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using Firebase.Database;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Powerup
{
    public delegate void PowerupsLoaded(List<PowerupEntity> powerups);

    class FireBasePowerupReader : IPowerupReader
    {
        public static PowerupsLoaded PowerupsLoaded;

        public void ReadPowerupsAsync()
        {
            try
            {
                FireBaseDatabase.Database.Child(FireBaseSavePaths.PlayerPowerupLocation())
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

                                            var result = new List<PowerupEntity>();

                                            foreach (var child in snapshot.Children)
                                            {
                                                foreach (var item in child.Children)
                                                {
                                                    string info = item?.GetRawJsonValue()?.ToString();

                                                    if (info != null)
                                                    {
                                                        var data = JsonUtility.FromJson<PowerupEntity>(info);
                                                        result.Add(data);
                                                    }
                                                }
                                            }

                                            PowerupsLoaded?.Invoke(result);
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
