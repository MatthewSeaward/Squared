using System;

namespace Assets.Scripts.Workers.IO.Data_Entities
{
    [Serializable]
    public class LevelProgress
    {
        public string Chapter;
        public int Level;
        public int StarAchieved;
        public int HighestScore;
    }
}
