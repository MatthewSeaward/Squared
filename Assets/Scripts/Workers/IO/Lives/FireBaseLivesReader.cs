using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using Firebase.Database;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Lives
{
    class FireBaseLivesReader : ILivesReader
    {

        public async Task ReadLivesAsync()
        {
            try
            {
                await FireBaseDatabase.Database.Child(FireBaseSavePaths.PlayerLivesLocation())
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

                                          LivesEntity result = null;
                                          if (info != null)
                                          {
                                              result = JsonUtility.FromJson<LivesEntity>(info);
                                          }

                                          LivesManager.Instance.Setup(result);
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
                Debug.LogError(ex);
            }
        }
    }
}
