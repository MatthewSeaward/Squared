using System;

namespace Assets.Scripts.Workers.IO.Data_Entities
{
    [Serializable]
    public class ScoreEntry
    {
        public string User;       
        public string Date;
        public int Score;
    }
}