using System;
using System.Collections.Generic;
using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.IO;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Lives;
using Assets.Scripts.Workers.IO.Powerup;
using Assets.Scripts.Workers.Powerups.Interfaces;

namespace Assets.Scripts.Workers
{
    public delegate void DataLoaded();

    public class UserIO
    {
        private bool piecesCollectedLoaded;
        private bool powerupsLoaded;
        private bool equippedPowerupsLoaded;
        private bool livesLoaded;

        private bool LoadComplete => piecesCollectedLoaded && powerupsLoaded && equippedPowerupsLoaded && livesLoaded;

        public static DataLoaded DataLoaded;

        private IPowerupWriter powerupWriter = new FireBasePowerupWriter();
        private IPowerupReader powerupReader = new FireBasePowerupReader();
        private ILivesWriter livesWriter = new FireBaseLivesWriter();
        private ILivesReader livesReader = new FireBaseLivesReader();

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
            FireBasePowerupReader.EquippedPowerupsLoaded += FireBaseEquippedPowerupsLoaded;
            FireBaseLivesReader.LivesLoaded += FireBaseLivesLoaded;

            ResetData.ResetAllData += ResetSavedData;
        }


        ~UserIO()
        {
            PieceCollectionIO.PiecesCollectedLoadedEvent -= PiecesCollectedLoadedEventHandler;
            FireBasePowerupReader.PowerupsLoaded -= FireBasePowerupsLoaded;
            FireBasePowerupReader.EquippedPowerupsLoaded -= FireBaseEquippedPowerupsLoaded;
            FireBaseLivesReader.LivesLoaded -= FireBaseLivesLoaded;

            ResetData.ResetAllData -= ResetSavedData;
        }

        public void SavePowerupInfo()
        {
            powerupWriter.WritePowerups(UserPowerupManager.Instance.Powerups);
        }

        public void SaveEquippedPowerupInfo()
        {
            powerupWriter.WriteEquippedPowerups(UserPowerupManager.Instance.EquippedPowerups);
        }

        public void SaveLivesInfo()
        {
            livesWriter.WriteLives(LivesManager.LivesRemaining, DateTime.Now);
        }

        private void LoadPowerupData()
        {
            powerupReader.ReadPowerupsAsync();
            powerupReader.ReadEquippedPowerupsAsync();
        }

        public void LoadData()
        {
            LoadPowerupData();
            livesReader.ReadLivesAsync();

            PieceCollectionIO.Instance.LoadUserData();
        }


        private void FireBaseLivesLoaded(LivesEntity livesEntity)
        {
            if (livesEntity != null)
            {
                LivesManager.Setup(livesEntity);
            }

            livesLoaded = true;

            if (LoadComplete)
            {
                DataLoaded?.Invoke();
            }
        }

        private void PiecesCollectedLoadedEventHandler()
        {
            powerupsLoaded = true;

            if (LoadComplete)
            {
                DataLoaded?.Invoke();
            }
        }

        private void FireBaseEquippedPowerupsLoaded(string[] powerups)
        {
            for (int i = 0; i < powerups.Length; i++)
            {
                UserPowerupManager.Instance.EquippedPowerups[i] = PowerupFactory.GetPowerup(powerups[i]);
            }

            equippedPowerupsLoaded = true;
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
