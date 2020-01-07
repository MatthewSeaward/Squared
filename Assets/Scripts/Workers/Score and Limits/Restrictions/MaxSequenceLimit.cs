﻿using Assets.Scripts.Workers.Score_and_Limits.Interfaces;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class MaxSequenceLimit : IRestriction
    {
        public readonly int MaxLimit;
        private bool reachedLimit = false;
        public bool Ignored { get; private set; }

        public MaxSequenceLimit(int limit)
        {
            this.MaxLimit = limit;
        }

        public string GetRestrictionText()
        {
            return "Max Sequence: " + MaxLimit;
        }

        public string GetRestrictionDescription() => GetRestrictionText();

        public bool ViolatedRestriction()
        {
            return reachedLimit;
        }

        public void Reset()
        {
            reachedLimit = false;
        }

        public void SequenceCompleted(ISquarePiece[] sequence)
        {
            reachedLimit = IsRestrictionViolated(sequence);

            Ignored = false;
        }

        public bool IsRestrictionViolated(ISquarePiece[] sequence)
        {
           if (Ignored)
           {
                return false;
           }

           return sequence.Length > MaxLimit;
        }
        
        public void Ignore()
        {
            Ignored = true;
        }
    }
}
