using System;

namespace Assets.Scripts.Workers.Daily_Challenge
{
    public class CustomDayGenerator : INumberGenerator
    {
        private readonly Random random;

        public CustomDayGenerator(int day, int month, int year)
        {
            random = new Random(Convert.ToInt32($"{day}{month}{year}"));
        }

        public int Next()
        {
            return random.Next();
        }

        public int Range(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
