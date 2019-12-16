﻿using Assets.Scripts.Workers.IO.Data_Entities;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Player_Progress
{
    class PlayerPrefsLevelProgressLoader : ILevelProgressReader
    {
        public LevelProgress[] LoadLevelProgress()
        {
            var playerPrefs = PlayerPrefs.GetString("LevelProgress");
            if (string.IsNullOrEmpty(playerPrefs))
            {
                return new LevelProgress[0];
            }

            var fromJSON = JsonHelper.FromJson<LevelProgress>(playerPrefs);
            if (fromJSON == null)
            {
                return new LevelProgress[0];
            }

            return fromJSON;
        }

        public void LoadLevelProgressAsync()
        {
            throw new System.NotImplementedException();
        }

        public void ResetData()
        {
            PlayerPrefs.DeleteAll();
        }

        public void SaveLevelProgress(LevelProgress[] levelProgress)
        {
            var toJson = JsonHelper.ToJson(levelProgress);
            PlayerPrefs.SetString("LevelProgress", toJson);

        }
    }
}
