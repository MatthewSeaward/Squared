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

        public bool WithinRange(int input)
        {
            return input >= Min && input <= Max;
        }
    }
}
