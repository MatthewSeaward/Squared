using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO.Helpers;
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
        public void SaveScore(string chapter, int level, int star, int score, GameResult result)
        {            
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

            var key = FireBaseDatabase.Database.Child("Scores").Push().Key;

            FireBaseDatabase.Database.Child("Scores").Child(chapter).Child("LVL " + level).Child("Star " +star).Child(key).SetRawJsonValueAsync(jsonValue);
        }

        public void WriteData(string path, string data)
        {
            string key = FireBaseDatabase.Database.Child(path).Push().Key;

            FireBaseDatabase.Database.Child(path + $"/{key}").SetValueAsync(data);
        }
        
    }
}
