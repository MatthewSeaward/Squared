using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using UnityEngine;

namespace Assets.Scripts.Workers.IO
{
    class FireBaseLevelProgressWriter : ILevelProgressWriter
    {
        public void ResetData()
        {
            SaveLevelProgress(new LevelProgress[0]);
        }

        public void SaveLevelProgress(LevelProgress[] levelProgress)
        {
            var toJson = JsonHelper.ToJson(levelProgress);

            var result = System.Threading.Tasks.Task.Run(() => FireBaseDatabase.Database.Child(FireBaseSavePaths.PlayerProgressLocation()).SetRawJsonValueAsync(toJson));
            if (result.IsCanceled || result.IsFaulted)
            {
                Debug.LogWarning(result.Exception);
            }
        }
    }
}
