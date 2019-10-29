using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Workers.Data_Managers
{
    public delegate void PowerupCountChanged(IPowerup powerup);

    public class UserPowerupManager
    {
        public static PowerupCountChanged PowerupCountChanged;

        private static UserPowerupManager _instance;

        public static UserPowerupManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserPowerupManager();
                }
                return _instance;
            }
        }

        public List<PowerupCollection> Powerups = new List<PowerupCollection>();

        private UserPowerupManager()
        {
            PieceCollectionManager.PieceCollectionComplete += PieceCollectionCompleteHandler;
        }
        
        ~UserPowerupManager()
        {
            PieceCollectionManager.PieceCollectionComplete -= PieceCollectionCompleteHandler;
        }

        private void PieceCollectionCompleteHandler(SquarePiece.Colour type)
        {
            AddNewPowerup(PowerupFactory.GetPowerup(type));
        }

        public void AddNewPowerup(IPowerup powerup)
        {
            var match = Powerups.FirstOrDefault(x => x.Powerup.GetType() == powerup.GetType());
            if (match == null)
            {
                Powerups.Add(new PowerupCollection() { Powerup = powerup, Count = 1 });
            }
            else
            {
                match.Count++;
            }
            UserIO.SavePowerupInfo();
            PowerupCountChanged?.Invoke(powerup);
        }

        public void UsePowerup(IPowerup powerup)
        {
            var match = Powerups.FirstOrDefault(x => x.Powerup.GetType() == powerup.GetType());
            if (match != null)
            {
               match.Count--;
               if (match.Count < 0)
               {
                   match.Count = 0;
               }
            }
            UserIO.SavePowerupInfo();
            PowerupCountChanged?.Invoke(powerup);
        }

        public int GetUses(IPowerup powerup)
        {
            var match = Powerups.FirstOrDefault(x => x.Powerup.GetType() == powerup.GetType());
            if (match != null)
            {
                return match.Count;
            }
            else
            {
                return 0;
            }
        }            
    }
}