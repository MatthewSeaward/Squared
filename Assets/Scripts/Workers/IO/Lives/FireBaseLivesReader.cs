using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using Firebase.Database;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Lives
{
    public delegate void LivesLoaded(LivesEntity livesEntity);

    class FireBaseLivesReader : ILivesReader
    {
        public static LivesLoaded LivesLoaded;

        public void ReadLivesAsync()
        {
            try
            {
                FireBaseDatabase.Database.Child(FireBaseSavePaths.PlayerLivesLocation())
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

                                          LivesLoaded?.Invoke(result);

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
