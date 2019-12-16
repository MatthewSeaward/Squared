using Assets.Scripts.Workers.IO.Data_Entities;
using System.Threading.Tasks;

namespace Assets.Scripts.Workers.IO
{
    interface ILevelProgressReader
    {
        LevelProgress[] LoadLevelProgress();
        void LoadLevelProgressAsync();
    }
}
