using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Assets.Scripts.Workers.IO.Powerup
{
    public delegate void PowerupsLoaded(List<PowerupEntity> powerups);

    class FireBasePowerupReader : IPowerupReader
    {
        public static PowerupsLoaded PowerupsLoaded;

        public void ReadPowerupsAsync()
        {
            try
            {
                var t = new Task(() =>
                {
                    var result = FireBaseReader.ReadAsync<PowerupEntity>(FireBaseSavePaths.PlayerPowerupLocation());
                    PowerupsLoaded?.Invoke(result.Result);
                });
                t.Start();

            }
            catch (Exception ex)
            {
            }
        }
    }
}
