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
                case Colour.Pink:
                    return new ExtraLife();
                case Colour.DarkBlue:
                    return new IgnoreRestriction();
                case Colour.Green:
                    return new ExtraTime();
                case Colour.Grey:
                    return new SpecialPieces();
                case Colour.LightBlue:
                    return new MinePowerup();
                case Colour.Orange:
                    return new PerformBestMove();
            }
            return null;
        }        
    }
}
