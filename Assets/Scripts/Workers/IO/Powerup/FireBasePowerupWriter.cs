using System.Collections.Generic;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Powerup
{
    class FireBasePowerupWriter : IPowerupWriter
    {
        public void WritePowerups(List<PowerupCollection> powerups)
        {
            var data = new List<PowerupEntity>();
            foreach (PowerupCollection powerupCollected in powerups)
            {
                data.Add(new PowerupEntity() { Name = powerupCollected.Powerup.GetType().Name, Count = powerupCollected.Count });
            }

            var toJson = JsonHelper.ToJson(data.ToArray());

            var result = System.Threading.Tasks.Task.Run(() => FireBaseDatabase.Database.Child(FireBaseSavePaths.PlayerPowerupLocation()).SetRawJsonValueAsync(toJson));
            if (result.IsCanceled || result.IsFaulted)
            {
                Debug.LogWarning(result.Exception);
            }
        }
    }
}
