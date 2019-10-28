using System.Linq;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class DiagonalOnlyRestriction : IRestriction
    {
        private bool _violatedRestriction = false;
        private bool ignored;

        public string GetRestrictionText()
        {
            return "Only diagonal connections";
        }

        public string GetRestrictionDescription() => GetRestrictionText();

        public void Reset()
        {
            _violatedRestriction = false;
        }

        public void SequenceCompleted(ISquarePiece[] sequence)
        {
            _violatedRestriction = IsRestrictionViolated(sequence);
            ignored = false;
        }

        public void Update(float deltaTime)
        {
            
        }

        public bool ViolatedRestriction()
        {
            return _violatedRestriction;
        }

        public bool IsRestrictionViolated(ISquarePiece[] sequence)
        {
            if (ignored)
            {
                return false;
            }

            var seqArray = sequence.ToArray();

            for (int i = 0; i < seqArray.Length; i++)
            {
                if (i + 1 >= seqArray.Length)
                {
                    continue;
                }

                var firstPiece = seqArray[i];
                var secondPiece = seqArray[i + 1];

                if (!ConnectionHelper.DiagonalConnection(firstPiece, secondPiece))
                {
                   return true;
                }
            }

            return false;
        }
        
        public void Ignore()
        {
            ignored = true;
        }
    }
}
