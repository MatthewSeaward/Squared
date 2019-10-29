using System;
using System.Collections.Generic;
using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.IO;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Powerup;

namespace Assets.Scripts.Workers
{
    public delegate void DataLoaded();

    public static class UserIO
    {
        private static bool piecesCollectedLoaded;
        private static bool powerupsLoaded;

        private static bool LoadComplete => piecesCollectedLoaded && powerupsLoaded;

        public static DataLoaded DataLoaded;

        private static IPowerupWriter powerupWriter = new FireBasePowerupWriter();
        private static IPowerupReader powerupReader = new FireBasePowerupReader();

        public static void SavePowerupInfo()
        {
            powerupWriter.WritePowerups(UserPowerupManager.Instance.Powerups);
        }

        private static void ReadPowerupInfo()
        {
            FireBasePowerupReader.PowerupsLoaded += FireBasePowerupsLoaded;
            powerupReader.ReadPowerupsAsync();
        }

        public static void LoadData()
        {
            ReadPowerupInfo();

            PieceCollectionIO.PiecesCollectedLoadedEvent += PiecesCollectedLoadedEventHandler;
            PieceCollectionIO.LoadUserData();
        }

        private static void PiecesCollectedLoadedEventHandler()
        {
            PieceCollectionIO.PiecesCollectedLoadedEvent -= PiecesCollectedLoadedEventHandler;
            powerupsLoaded = true;

            if (LoadComplete)
            {
                DataLoaded?.Invoke();
            }
        }

        private static void FireBasePowerupsLoaded(List<PowerupEntity> powerups)
        {
            FireBasePowerupReader.PowerupsLoaded -= FireBasePowerupsLoaded;

            foreach (var entity in powerups)
            {
                UserPowerupManager.Instance.Powerups.Add(new PowerupCollection() { Powerup =  PowerupFactory.GetPowerup(entity.Name), Count = entity.Count });
            }
            piecesCollectedLoaded = true;

            if (LoadComplete)
            {
                DataLoaded?.Invoke();
            }
        }
    }
}
