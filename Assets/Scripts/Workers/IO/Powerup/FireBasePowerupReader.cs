using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using Firebase.Database;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Powerup
{

    class FireBasePowerupReader : IPowerupReader
    {
        public async Task ReadPowerupsAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    var result = FireBaseReader.ReadAsync<PowerupEntity>(FireBaseSavePaths.PlayerPowerupLocation());

                    foreach (var entity in result.Result)
                    {
                        UserPowerupManager.Instance.Powerups.Add(new PowerupCollection() { Powerup = PowerupFactory.GetPowerup(entity.Name), Count = entity.Count });
                    }
                });
            }
            catch (Exception ex)
            {
                DebugLogger.Instance.WriteException(ex);
            }
        }

        public async Task ReadEquippedPowerupsAsync()
        {
            try
            {
                await FireBaseDatabase.Database.Child(FireBaseSavePaths.PlayerEquippedPowerupLocation())
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

                                          for (int i = 0; i < result.Length; i++)
                                          {
                                              UserPowerupManager.Instance.EquippedPowerups[i] = PowerupFactory.GetPowerup(result[i]);
                                          }

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
                DebugLogger.Instance.WriteException(ex);
            }
        }
    }
}
