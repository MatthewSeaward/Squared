using Assets.Scripts.Workers.IO.Data_Entities;

namespace Assets.Scripts.Workers.IO
{
    interface ILevelProgressLoader
    {
        LevelProgress[] LoadLevelProgress();
        void SaveLevelProgress(LevelProgress[] levelProgress);
        void ResetData();
    }
}
