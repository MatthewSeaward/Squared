using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Lives
{
    class FireBaseLivesWriter : ILivesWriter
    {
        public void WriteLives(int livesRemaining, DateTime lastEarnedLife)
        {
            var entity = new LivesEntity() { LastEarnedLife = lastEarnedLife.ToString(), LivesRemaining = livesRemaining };

            var toJson = JsonUtility.ToJson(entity);

            var result = System.Threading.Tasks.Task.Run(() => FireBaseDatabase.Database.Child(FireBaseSavePaths.PlayerLivesLocation()).SetRawJsonValueAsync(toJson));
            if (result.IsCanceled || result.IsFaulted)
            {
                Debug.LogWarning(result.Exception);
            }
        }
    }
}
