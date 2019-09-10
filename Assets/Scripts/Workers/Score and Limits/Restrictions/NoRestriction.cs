﻿using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class NoRestriction : IRestriction
    {
        public string GetRestrictionText()
        {
            return "No Restriction";
        }

        public void Reset()
        {
        }

        public void SequenceCompleted(LinkedList<ISquarePiece> sequence)
        {
        }

        public void Update(float deltaTime)
        {
        }

        public bool ViolatedRestriction()
        {
            return false;
        }
    }
}
