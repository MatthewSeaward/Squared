using System.Collections.Generic;

namespace Assets.Scripts.Workers.Score_and_Limits.Interfaces
{
    public interface IRestriction
    {
        void Reset();
        void Update(float deltaTime);
        void SequenceCompleted(LinkedList<ISquarePiece> sequence);
        string GetRestrictionText();
        string GetRestrictionDescription();
        bool ViolatedRestriction();
    }
}