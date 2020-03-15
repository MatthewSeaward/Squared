using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly Colour[] _startingColours;
        public string[] Pattern;
        public string[] SpecialDropPieces;
        public int StarsToUnlock;
        public int StarAchieved;

        public StarProgress Star1Progress = new StarProgress();
        public StarProgress Star2Progress = new StarProgress();
        public StarProgress Star3Progress = new StarProgress();

        public List<Colour> Colours { get; set;} = new List<Colour>();

        public Level()
        {

        }

        public Level(IO.Data_Entities.Level dataEntities)
        {
            this.LevelNumber = dataEntities.LevelNumber;
            this.Target = dataEntities.Target;

            _startingColours = dataEntities?.colours ?? new Colour[0];

            this.Colours = dataEntities?.colours?.ToList() ?? new List<Colour>();
            this.Pattern = dataEntities?.Pattern ?? new string[0];
            this.SpecialDropPieces = dataEntities?.SpecialDropPieces ?? new string[0];
            this.StarsToUnlock = dataEntities?.StarsToUnlock ?? 0;
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
            if (StarAchieved == 1)
            {
                return Star2Progress;
            }
            else if (StarAchieved >= 2)
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

        public void Reset()
        {
            Colours = _startingColours?.ToList() ?? new List<Colour>();
        }
    }
}
