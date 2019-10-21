using Assets.Scripts.Workers.Score_and_Limits.Interfaces;

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
        }

        public void Update(float deltaTime)
        {
            
        }

        public bool IsRestrictionViolated(ISquarePiece[] sequence)
        {
            return sequence.Length < MinLimit;
         }
    }
}
