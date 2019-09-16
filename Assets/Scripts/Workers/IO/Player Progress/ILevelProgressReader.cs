using Assets.Scripts.Workers.IO.Data_Entities;

namespace Assets.Scripts.Workers.IO
{
    interface ILevelProgressReader
    {
        LevelProgress[] LoadLevelProgress();
        void LoadLevelProgressAsync();

    }
}
