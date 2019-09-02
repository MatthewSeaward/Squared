using Assets.Scripts.Workers.Data_Managers;
using DataEntities;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.IO
{
    public class FireBaseScoreLoader : IScoreLoader
    {
        private DatabaseReference Database;

        public void SaveScore(string chapter, int level, int star, int score, GameResult result)
        {
            GetDatabase();
            
            var entry = new ScoreEntry()
            {
                Chapter = chapter,
                Level = level,
                Star = star,
                Date = DateTime.Now.ToString(),
                Result = result,
                Score = score,
                User = UserManager.UserID
            };

            var jsonValue = JsonUtility.ToJson(entry);

            var key = Database.Child("Scores").Push().Key;

            Database.Child("Scores").Child(key).SetRawJsonValueAsync(jsonValue);
        }

        public void WriteData(string path, string data)
        {
             GetDatabase();
            string key = Database.Child(path).Push().Key;

             Database.Child(path + $"/{key}").SetValueAsync(data);
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
