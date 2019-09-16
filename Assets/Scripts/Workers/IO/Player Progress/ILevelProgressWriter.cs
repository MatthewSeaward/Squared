using Assets.Scripts.Workers.IO.Data_Entities;

namespace Assets.Scripts.Workers.IO
{
    public interface ILevelProgressWriter
    {
        void SaveLevelProgress(LevelProgress[] levelProgress);
        void ResetData();
    }
}
