using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO.Lives;
using Assets.Scripts.Workers.IO.Powerup;

namespace Assets.Scripts.Workers
{
    public delegate void DataLoaded();

    public class UserIO
    {
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
            ResetData.ResetAllData += ResetSavedData;
        }

        ~UserIO()
        {
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
            livesWriter.WriteLives(LivesManager.Instance.LivesRemaining, LivesManager.Instance.LastEarnedLife);
        }

        public void LoadPowerupsData()
        {
            powerupReader.ReadPowerupsAsync();
        }    

        public void LoadEquippedPowerups()
        {
            powerupReader.ReadEquippedPowerupsAsync();
        }

        public void LoadLives()
        {
            livesReader.ReadLivesAsync();
        }     
   
        private void ResetSavedData()
        {
            UserPowerupManager.Instance.Powerups.Clear();
            SavePowerupInfo();
        }
    }
}
