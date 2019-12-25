using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using Firebase.Database;
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Workers.IO
{
    public class FireBaseLevelProgressReader : ILevelProgressReader
    {
        public async Task<LevelProgress[]> LoadLevelProgress()
        {
            try
            {
                return await FetchDataFromFireBase();               
            }
            catch (Exception ex)
            {
                Debug.LogError(ex);
            }

            return null;
        }

        private async static Task<LevelProgress[]> FetchDataFromFireBase()
        {
            LevelProgress[] result = null;

            await FireBaseDatabase.Database.Child(FireBaseSavePaths.PlayerProgressLocation())
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

                                                     if (info != null)
                                                     {
                                                         result =  JsonHelper.FromJson<LevelProgress>(info);
                                                     }
                                                 }
                                                 catch (Exception ex)
                                                 {
                                                     Debug.LogError(ex);
                                                 }
                                             }
                                         });
            return result;
        }
       
    }
}
