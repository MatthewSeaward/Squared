using Assets.Scripts.Workers.Score_and_Limits.Interfaces;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class MinSequenceLimit : IRestriction
    {
        public readonly int MinLimit;
        private bool reachedLimit = false;
        public bool Ignored { get; private set; }

        public MinSequenceLimit(int limit)
        {
            this.MinLimit = limit;
        }

        public string GetRestrictionText()
        {
            return "Min Sequence: " + MinLimit;
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

            return sequence.Length < MinLimit;
        }
        
        public void Ignore()
        {
            Ignored = true;
        }
    }
}
