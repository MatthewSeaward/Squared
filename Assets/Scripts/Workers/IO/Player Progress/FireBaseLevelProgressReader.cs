using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using Firebase.Database;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.IO
{
    public delegate void UserDataLoaded(LevelProgress[] levelProgresses);

    public class FireBaseLevelProgressReader : ILevelProgressReader
    {
        public static UserDataLoaded UserDataLoaded;

        public LevelProgress[] LoadLevelProgress()
        {
            throw new NotImplementedException();
        }

        public void LoadLevelProgressAsync()
        {
            try
            {
                FireBaseDatabase.Database.Child(FireBaseSavePaths.PlayerProgressLocation())
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

                                        var result = new LevelProgress[0];
                                        if (info != null)
                                         {
                                             result = JsonHelper.FromJson<LevelProgress>(info);
                                         }

                                         UserDataLoaded?.Invoke(result);

                                     }
                                     catch(Exception ex)
                                     {
                                         Debug.LogError(ex);
                                     }
                                 }
                             });
            }
            catch {  }
        }

    }
}
