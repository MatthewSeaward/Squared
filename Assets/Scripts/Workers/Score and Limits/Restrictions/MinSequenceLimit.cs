using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class MinSequenceLimit : IRestriction
    {
        private readonly int MinLimit;
        private bool reachedLimit = false;

        public MinSequenceLimit(int limit)
        {
            this.MinLimit = limit;
        }

        public string GetDescription()
        {
            return $"Sequences less than {MinLimit}";
        }

        public string GetRestrictionText()
        {
            return $"Min Sequence: {MinLimit}";
        }

        public bool ViolatedRestriction()
        {
            return reachedLimit;
        }

        public void Reset()
        {
            reachedLimit = false;
        }

        public void SequenceCompleted(LinkedList<ISquarePiece> sequence)
        {
            reachedLimit = sequence.Count < MinLimit;
        }

        public void Update(float deltaTime)
        {
            
        }
    }
}
