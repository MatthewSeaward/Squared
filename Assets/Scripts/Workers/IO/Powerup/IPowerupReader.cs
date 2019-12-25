using System.Threading.Tasks;

namespace Assets.Scripts.Workers.IO.Powerup
{
    interface IPowerupReader
    {
        Task ReadPowerupsAsync();
        Task ReadEquippedPowerupsAsync();
    }
}