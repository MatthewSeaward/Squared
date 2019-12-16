using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Workers.IO
{
    public class FireBaseLevelProgressReader : ILevelProgressReader
    {
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
                                             UserDataLoaded(result);
                                         }
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

        private void UserDataLoaded(LevelProgress[] levelProgresses)
        {
            var LevelProgress = new List<LevelProgress>();
            if (levelProgresses != null)
            {
                LevelProgress.AddRange(levelProgresses);
            }

            foreach (var progress in LevelProgress)
            {
                if (!LevelManager.Instance.Levels.ContainsKey(progress.Chapter))
                {
                    continue;
                }
                LevelManager.Instance.Levels[progress.Chapter][progress.Level].LevelProgress = progress;
            }
        }
    }
}
