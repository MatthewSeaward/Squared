﻿using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class MaxSequenceLimit : IRestriction
    {
        private readonly int MaxLimit;
        private bool reachedLimit = false;

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
            reachedLimit = sequence.Length > MaxLimit;
        }

        public void Update(float deltaTime)
        {

        }
    }

}
