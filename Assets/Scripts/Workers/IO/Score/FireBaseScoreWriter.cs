using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO.Helpers;
using DataEntities;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.IO
{
    public class FireBaseScoreWriter : IScoreWriter
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

            FireBaseDatabase.AddUniqueJSON(FireBaseSavePaths.ScoreLocation(chapter, level, star), jsonValue);        
        }        
    }
}
