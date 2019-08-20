using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class NoRestriction : IRestriction
    {
        public string GetDescription()
        {
            return "No Restriction";
        }

        public string GetRestrictionText()
        {
            return "";
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
