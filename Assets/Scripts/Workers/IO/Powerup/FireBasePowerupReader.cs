using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Powerup
{
    public delegate void PowerupsLoaded(List<PowerupEntity> powerups);
    public delegate void EquippedPowerupsLoaded(string[] powerups);

    class FireBasePowerupReader : IPowerupReader
    {
        public static PowerupsLoaded PowerupsLoaded;
        public static EquippedPowerupsLoaded EquippedPowerupsLoaded;

        public void ReadPowerupsAsync()
        {
            try
            {
                var t = new Task(() =>
                {
                    var result = FireBaseReader.ReadAsync<PowerupEntity>(FireBaseSavePaths.PlayerPowerupLocation());
                    PowerupsLoaded?.Invoke(result.Result);
                });
                t.Start();

            }
            catch (Exception ex)
            {
            }
        }

        public void ReadEquippedPowerupsAsync()
        {
            try
            {
                FireBaseDatabase.Database.Child(FireBaseSavePaths.PlayerEquippedPowerupLocation())
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
                                          
                                          string info = snapshot?.GetRawJsonValue()?.ToString();

                                          var result = new string[0];
                                          if (info != null)
                                          {
                                              result = JsonHelper.FromJson<string>(info);
                                          }

                                          EquippedPowerupsLoaded?.Invoke(result);

                                      }
                                      catch (Exception ex)
                                      {
                                          Debug.LogError(ex);
                                      }
                                  }
                              });

            }
            catch (Exception ex)
            {
            }
        }
    }
}
