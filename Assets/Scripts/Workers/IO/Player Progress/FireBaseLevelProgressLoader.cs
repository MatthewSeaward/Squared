using Assets.Scripts.Workers.IO.Data_Entities;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.IO
{
    public delegate void UserDataLoaded(LevelProgress[] levelProgresses);

    public class FireBaseLevelProgressLoader : ILevelProgressLoader
    {
        public static UserDataLoaded UserDataLoaded;

        DatabaseReference Database = null;

        public LevelProgress[] LoadLevelProgress()
        {
            GetDatabase();

            var result =  new LevelProgress[0];
            try
            {
                FirebaseDatabase.DefaultInstance
                             .GetReference("LevelProgress/Me")
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

                                         string info = snapshot.GetRawJsonValue().ToString();
                                         result = JsonHelper.FromJson<LevelProgress>(info);
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

            return result;
        }


        public void SaveLevelProgress(LevelProgress[] levelProgress)
        {
            GetDatabase();

            var toJson = JsonHelper.ToJson(levelProgress);

           var result = System.Threading.Tasks.Task.Run(() => Database.Child("LevelProgress").Child("Me").SetRawJsonValueAsync(toJson));
            if (result.IsCanceled || result.IsFaulted)
            {
                Debug.LogWarning(result.Exception);
            }
        }

        private void GetDatabase()
        {
            if (Database != null)
            {
                return;
            }
            FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://squared-105cf.firebaseio.com/");

            // Get the root reference location of the database.
            Database = FirebaseDatabase.DefaultInstance.RootReference;

        }

    }
}
