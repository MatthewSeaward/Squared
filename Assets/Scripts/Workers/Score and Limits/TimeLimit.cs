using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class TimeLimit : IGameLimit
    {
        private int Limit;
        private float TimeTaken;

        public TimeLimit(int limit)
        {
            this.Limit = limit;
        }

        public string GetDescription()
        {
            return $"{Limit} seconds";
        }

        public string GetLimitText()
        {
            var timeLeft = Limit - TimeTaken;
            return $"Time Left: {timeLeft.ToString("0.00")}";
        }

        public bool ReachedLimit()
        {
            return TimeTaken > Limit;
        }

        public void Reset()
        {
            TimeTaken = 0f;
        }

        public void SequenceCompleted(LinkedList<ISquarePiece> sequence)
        {
        }

        public void Update(float deltaTime)
        {
            TimeTaken += deltaTime;
        }
    }
}
