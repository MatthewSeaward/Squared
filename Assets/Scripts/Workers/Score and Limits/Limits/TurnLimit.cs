using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System;
using System.Collections.Generic;

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
            return $"Turns Left: {MaxTurns- TurnsTaken}";
        }

        public void SequenceCompleted(LinkedList<ISquarePiece> sequence)
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
            return $"{MaxTurns} turns";
        }

        public void Reset()
        {
            TurnsTaken = 0;
        }
    }
}
