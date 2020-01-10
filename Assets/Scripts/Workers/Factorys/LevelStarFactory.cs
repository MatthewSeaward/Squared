using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System;

namespace Assets.Scripts.Workers.Factorys
{
    public static class LevelStarFactory
    {

        public static StarProgress GetLevelStar(int starNumber, string[] data, LevelEvents.EventTrigger[] events)
        {
            var progress = new StarProgress() { Number = starNumber };

            if (data != null)
            {
                progress.Limit = GetStarGameLimit(data);
                progress.Restriction = GetStarRestriction(data);
            }

            if (events != null)
            {
                progress.Events = EnemyEventsFactory.GetLevelEvents(events);
            }

            return progress;
        }

        private static IGameLimit GetStarGameLimit(string[] parts)
        {
            if (parts.Length < 2)
            {
                return null;
            }

            var type = parts[0];
            var element = parts[1];

            switch (type)
            {
                case "Turns":
                    return new TurnLimit(Convert.ToInt32(parts[1]));
                case "Time":
                    return new TimeLimit(Convert.ToInt32(parts[1]));
            }
            return null;
        }

        private static IRestriction GetStarRestriction(string[] parts)
        {
            if (parts.Length < 4)
            {
                return new NoRestriction();
            }

            var type = parts[2];
            var element = parts[3];

            switch (type)
            {
                case "Min":
                    return new MinSequenceLimit(Convert.ToInt32(element));
                case "Max":
                    return new MaxSequenceLimit(Convert.ToInt32(element));
                case "Banned":
                    return new BannedSprite(Convert.ToInt32(element));
                case "Type":
                    return new BannedPieceType(element);
                case "Banned Locked":
                    return new SwapEffectLimit();
                case "No Diagonal":
                    return new DiagonalRestriction();
                case "Diagonal Only":
                    return new DiagonalOnlyRestriction();
                case "Time":
                    return new TurnTimeLimit(Convert.ToSingle(element));
                default:
                    return new NoRestriction();
            }
        }
    }
}
