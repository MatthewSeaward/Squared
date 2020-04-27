using System;

namespace Assets.Scripts.Workers.Daily_Challenge
{
    public class CurrentDayGenerator : INumberGenerator
    {
        private readonly Random random;

        public CurrentDayGenerator()
        {
            string seed = $"{DateTime.Now.Day}{DateTime.Now.Month}{DateTime.Now.Year}";
            random = new Random(Convert.ToInt32(seed));
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
