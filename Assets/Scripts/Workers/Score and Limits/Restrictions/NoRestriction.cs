using Assets.Scripts.Workers.Score_and_Limits.Interfaces;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class NoRestriction : IRestriction
    {
        public string GetRestrictionText()
        {
            return "No Restriction";
        }

        public string GetRestrictionDescription() => GetRestrictionText();

        public void Reset()
        {
        }

        public void SequenceCompleted(ISquarePiece[] sequence)
        {
        }

        public void Update(float deltaTime)
        {
        }

        public bool ViolatedRestriction()
        {
            return false;
        }

        public bool IsRestrictionViolated(ISquarePiece[] sequence)
        {
            return false;
        }
    }
}
