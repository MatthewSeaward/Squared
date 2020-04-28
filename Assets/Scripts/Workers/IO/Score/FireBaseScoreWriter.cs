﻿using Assets.Scripts.Workers.IO.Helpers;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.IO.Data_Entities;
using System;
using UnityEngine;
using Assets.Scripts.Workers.IO.Score;

namespace Assets.Scripts.Workers.IO
{
    public class FireBaseScoreWriter : IScoreWriter
    {
        public void SaveDailyChallenge(int score)
        {
            var entry = new ScoreEntry()
            {
                User = UserManager.UserID,
                Score = score,
                Date = DateTime.Now.ToString()
            };

            var jsonValue = JsonUtility.ToJson(entry);

            FireBaseDatabase.AddUniqueJSON(FireBaseSavePaths.DailyChallengeLocation(DateTime.Now), jsonValue);
        }

        public void SaveScore(string chapter, int level, int star, int score, GameResult result)
        {            
            var entry = new LevelScoreEntry()
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
