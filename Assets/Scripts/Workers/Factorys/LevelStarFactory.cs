using Assets.Scripts.Workers.Score_and_Limits;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System;

namespace Assets.Scripts.Workers.IO
{
    public static class LevelStarFactory
    {

        public static IGameLimit GetStarGameLimit(string[] parts)
        {
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

        public static IRestriction GetStarRestriction(string[] parts)
        {
            if (parts.Length > 2)
            {
                var type = parts[2];
                var element = parts[3];

                switch (type)
                {
                    case "Min":
                        return new MinSequenceLimit(Convert.ToInt32(parts[3]));
                    case "Max":
                        return new MaxSequenceLimit(Convert.ToInt32(parts[3]));
                    case "Banned":
                        if (int.TryParse(element, out int enumValue))
                        { 
                            return new BannedSprite(enumValue);
                        }
                        break;
                    case "Type":
                            return new BannedPieceType(element);
                    case "Banned Locked":
                        return new SwapEffectLimit();
                    case "No Diagonal":
                        return new DiagonalRestriction();
                    case "Diagonal Only":
                        return new DiagonalOnlyRestriction();
                    case "Time":
                        return new TurnTimeLimit(Convert.ToSingle(parts[3]));
                    default:
                        return new NoRestriction();
                }
            }
            return new NoRestriction();
        }
    }
}
