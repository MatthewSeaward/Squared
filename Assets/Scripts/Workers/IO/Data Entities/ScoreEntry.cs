using System;

namespace Assets.Scripts.Workers.IO.Data_Entities
{
    [Serializable]
    public class ScoreEntry
    {
        public string User;
        public int Level;
        public int Star;
        public string Chapter;
        public string Date;
        public int Score;
        public GameResult Result;
    }
}