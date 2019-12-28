using System;
using static SquarePiece;

namespace DataEntities
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
