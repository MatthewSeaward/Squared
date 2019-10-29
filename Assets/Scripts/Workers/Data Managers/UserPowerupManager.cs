using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Workers.Data_Managers
{
    public static class UserPowerupManager
    {
        public static List<PowerupCollection> Powerups = new List<PowerupCollection>();

        public static void AddNewPowerup(IPowerup powerup)
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
        }

        public static void UsePowerup(IPowerup powerup)
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
        }

        public static int GetUses(IPowerup powerup)
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