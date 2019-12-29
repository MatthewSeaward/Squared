using System;
using static SquarePiece;

namespace Assets.Scripts.Workers.IO.Data_Entities
{
    [Serializable]
    public class Level
    {
        public int LevelNumber;
        public int Target;
        public Colour[] colours;
        public string[] Pattern;
        public string[] Star1;
        public string[] Star2;
        public string[] Star3;
        public string[] SpecialDropPieces;
        public int StarsToUnlock;

        public LevelDialogue DiaglogueItems { get; internal set; }
    }
}
