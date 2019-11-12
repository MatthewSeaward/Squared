using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.IO.Powerup
{
    interface IPowerupWriter
    {
        void WritePowerups(List<PowerupCollection> powerups);
        void WriteEquippedPowerups(IPowerup[] powerups);
    }
}
