using Assets.Scripts.Workers.Score_and_Limits.Interfaces;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class MaxSequenceLimit : IRestriction
    {
        private readonly int MaxLimit;
        private bool reachedLimit = false;
        private bool ignored;

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

            ignored = false;
        }

        public void Update(float deltaTime)
        {

        }

        public bool IsRestrictionViolated(ISquarePiece[] sequence)
        {
           if (ignored)
           {
                return false;
           }

           return sequence.Length > MaxLimit;
        }
        
        public void Ignore()
        {
            ignored = true;
        }
    }
}
