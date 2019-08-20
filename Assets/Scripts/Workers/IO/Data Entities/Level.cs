using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
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

        [NonSerialized]
        public LevelProgress LevelProgress;

        [NonSerialized]
        public StarProgress Star1Progress = new StarProgress();
        public StarProgress Star2Progress = new StarProgress();
        public StarProgress Star3Progress = new StarProgress();

        public LevelDialogue DiaglogueItems { get; internal set; }

        public IGameLimit GetCurrentLimit()
        {
          return GetCurrentStar().Limit;            
        }

        public IRestriction GetCurrentRestriction()
        {
            return GetCurrentStar().Restriction;
        }

        public StarProgress GetCurrentStar()
        {
            if (LevelProgress == null)
            {
                return Star1Progress;
            }

            if (LevelProgress.StarAchieved == 1)
            {
                return Star2Progress;
            }
            else if (LevelProgress.StarAchieved >= 2)
            {
                return Star3Progress;
            }
            else
            {
                return Star1Progress;
            }
        }
    }
}
