using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Linq;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class DiagonalRestriction : IRestriction
    {
        private bool _violatedRestriction;
        public bool Ignored { get; private set; }

        public string GetRestrictionText()
        {
            return "No diagonal connections";
        }

        public string GetRestrictionDescription() => GetRestrictionText();

        public void Reset()
        {
            _violatedRestriction = false;
        }

        public void SequenceCompleted(ISquarePiece[] sequence)
        {
            _violatedRestriction = IsRestrictionViolated(sequence);

            Ignored = false;
        }
        
        public bool ViolatedRestriction()
        {
            return _violatedRestriction;
        }

        public bool IsRestrictionViolated(ISquarePiece[] sequence)
        {
            if (Ignored)
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

                if (ConnectionHelper.DiagonalConnection(firstPiece, secondPiece))
                {
                    return true;
                }
            }

            return false;
        }

        public void Ignore()
        {
            Ignored = true;
        }
    }
}
