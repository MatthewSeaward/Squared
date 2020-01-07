namespace Assets.Scripts.Workers.Score_and_Limits.Interfaces
{
    public interface IRestriction
    {
        bool Ignored { get; }

        void Reset();
        void SequenceCompleted(ISquarePiece[] sequence);
        string GetRestrictionText();
        string GetRestrictionDescription();
        bool ViolatedRestriction();
        bool IsRestrictionViolated(ISquarePiece[] sequence);
        void Ignore();
    }
}