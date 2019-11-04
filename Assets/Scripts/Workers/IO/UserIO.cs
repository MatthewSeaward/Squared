using System.Collections.Generic;
using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.IO;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Powerup;

namespace Assets.Scripts.Workers
{
    public delegate void DataLoaded();

    public class UserIO
    {
        private bool piecesCollectedLoaded;
        private bool powerupsLoaded;

        private bool LoadComplete => piecesCollectedLoaded && powerupsLoaded;

        public static DataLoaded DataLoaded;

        private IPowerupWriter powerupWriter = new FireBasePowerupWriter();
        private IPowerupReader powerupReader = new FireBasePowerupReader();

        private static UserIO _instance;

        public static UserIO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserIO();
                }
                return _instance;
            }
        }

        private UserIO()
        {
            PieceCollectionIO.PiecesCollectedLoadedEvent += PiecesCollectedLoadedEventHandler;
            FireBasePowerupReader.PowerupsLoaded += FireBasePowerupsLoaded;
            ResetData.ResetAllData += ResetSavedData;
        }

        ~UserIO()
        {
            PieceCollectionIO.PiecesCollectedLoadedEvent -= PiecesCollectedLoadedEventHandler;
            FireBasePowerupReader.PowerupsLoaded -= FireBasePowerupsLoaded;
            ResetData.ResetAllData -= ResetSavedData;
        }

        public void SavePowerupInfo()
        {
            powerupWriter.WritePowerups(UserPowerupManager.Instance.Powerups);
        }

        private void ReadPowerupInfo()
        { 
            powerupReader.ReadPowerupsAsync();
        }

        public void LoadData()
        {
            ReadPowerupInfo();

            PieceCollectionIO.Instance.LoadUserData();
        }

        private void PiecesCollectedLoadedEventHandler()
        {
            powerupsLoaded = true;

            if (LoadComplete)
            {
                DataLoaded?.Invoke();
            }
        }

        private void FireBasePowerupsLoaded(List<PowerupEntity> powerups)
        {

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

        private void ResetSavedData()
        {
            UserPowerupManager.Instance.Powerups.Clear();
            SavePowerupInfo();
        }
    }
}
