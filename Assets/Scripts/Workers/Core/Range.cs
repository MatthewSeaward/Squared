using System;

namespace Assets.Scripts.Workers.Core
{
    public struct Range
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public Range(int min, int max)
        {
            this.Min = min;
            this.Max = max;
        }

        public Range(string input)
        {
            if (!input.Contains("-"))
            {
                throw new ArgumentException($"Input in incorrect format. Input was: {input}");
            }

            var parts = input.Split('-');

            if (int.TryParse(parts[0], out int min))
            {
                Min = min;
            }
            else
            {
                throw new ArgumentException($"Min in incorrect format. Input was: {input}");
            }

            if (int.TryParse(parts[1], out int max))
            {
                Max = max;
            }
            else
            {
                throw new ArgumentException($"Max in in incorrect format. Input was: {input}");
            }
        }

        public bool WithinRange(int input)
        {
            return input >= Min && input <= Max;
        }
    }
}
