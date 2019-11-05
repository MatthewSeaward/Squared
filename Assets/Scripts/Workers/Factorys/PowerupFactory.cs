using Assets.Scripts.Workers.Powerups;
using Assets.Scripts.Workers.Powerups.Interfaces;
using static SquarePiece;

namespace Assets.Scripts.Workers.Factorys
{
    public static class PowerupFactory
    {
        public static IPowerup GetPowerup(Colour pieceType)
        {
            switch (pieceType)
            {
                case Colour.Red:
                    return new ExtraPoints();
                case Colour.Orange:
                    return new PerformBestMove();
                case Colour.Yellow:
                    return new DoubleMove();
                case Colour.Pink:
                    return new ExtraLife();
                case Colour.Purple:
                    return new PaintColour();
                  case Colour.Grey:
                    return new SpecialPieces();
                case Colour.Green:
                    return new ExtraTime();
                case Colour.DarkBlue:
                    return new IgnoreRestriction();
                case Colour.LightBlue:
                    return new MinePowerup();              
               
            }
            return null;
        }        

        public static IPowerup GetPowerup(string name)
        {
            switch(name)
            {
                case nameof(ExtraLife):
                    return new ExtraLife();
                case nameof(IgnoreRestriction):
                    return new IgnoreRestriction();
                case nameof(ExtraTime):
                    return new ExtraTime();
                case nameof(SpecialPieces):
                    return new SpecialPieces();
                case nameof(MinePowerup):
                    return new MinePowerup();
                case nameof(PerformBestMove):
                    return new PerformBestMove();
                case nameof(ExtraPoints):
                    return new ExtraPoints();
                case nameof(DoubleMove):
                    return new DoubleMove();
                case nameof(PaintColour):
                    return new PaintColour();
            }
            return null;
        }

        public static IPowerup[] GetAllPowerups()
        {
            return new IPowerup[]
            {
                new ExtraLife(),
                new IgnoreRestriction(),
                new ExtraTime(),
                new SpecialPieces(),
                new MinePowerup(),
                new PerformBestMove(),
                new ExtraPoints(),
                new DoubleMove(),
                new PaintColour()
            };
        }
    }
}
