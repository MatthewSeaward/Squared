using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Workers.Score_and_Limits.Interfaces
{
    public interface IGameLimit
    {
        void Reset();
        void Update(float deltaTime);
        void SequenceCompleted(LinkedList<ISquarePiece> sequence);
        string GetLimitText();
        bool ReachedLimit();
        string GetDescription();
    }
}
