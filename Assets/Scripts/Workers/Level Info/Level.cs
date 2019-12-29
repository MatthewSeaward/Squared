using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using static SquarePiece;

namespace Assets.Scripts.Workers.Level_Info
{
    public class Level
    {
        public int LevelNumber;
        public int Target;
        public Colour[] colours;
        public string[] Pattern;
        public string[] SpecialDropPieces;
        public int StarsToUnlock;

        public LevelProgress LevelProgress;

        public StarProgress Star1Progress = new StarProgress();
        public StarProgress Star2Progress = new StarProgress();
        public StarProgress Star3Progress = new StarProgress();

        public Level()
        {

        }

        public Level(IO.Data_Entities.Level dataEntities)
        {
            this.LevelNumber = dataEntities.LevelNumber;
            this.Target = dataEntities.Target;
            this.colours = dataEntities.colours;
            this.Pattern = dataEntities.Pattern;
            this.SpecialDropPieces = dataEntities.SpecialDropPieces;
            this.StarsToUnlock = dataEntities.StarsToUnlock;
        }

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

        public int BannedPiece()
        {
            if (GetCurrentRestriction() is BannedSprite)
            {
                return (GetCurrentRestriction() as BannedSprite).SpriteValue;
            }
            else
            {
                return -1;
            }
        }
    }
}
