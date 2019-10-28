using Assets.Scripts.Workers.Score_and_Limits.Interfaces;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class TurnLimit : IGameLimit
    {
        private int TurnsTaken;
        private int MaxTurns;

        public TurnLimit (int maxTurns)
        {
            this.MaxTurns = maxTurns;
        }
 
        public string GetLimitText()
        {
            return "Turns Left: " + (MaxTurns- TurnsTaken);
        }

        public void SequenceCompleted(ISquarePiece[] sequence)
        {
            TurnsTaken++;
        }

        public bool ReachedLimit()
        {
            return TurnsTaken >= MaxTurns;
        }

        public void Update(float deltaTime)
        {
        }

        public string GetDescription()
        {
            return MaxTurns + " turns";
        }

        public void Reset()
        {
            TurnsTaken = 0;
        }

        public float PercentComplete()
        {
            return ((float) TurnsTaken / (float) MaxTurns) * 100;
        }

        public void Increase(int amount)
        {
            MaxTurns += amount;
        }
    }
}
