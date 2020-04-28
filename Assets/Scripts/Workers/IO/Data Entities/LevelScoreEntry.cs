using System;

namespace Assets.Scripts.Workers.IO.Data_Entities
{
    [Serializable]
    public class LevelScoreEntry : ScoreEntry
    {
        public int Level;
        public int Star;
        public string Chapter;
        public GameResult Result;
    }
}
