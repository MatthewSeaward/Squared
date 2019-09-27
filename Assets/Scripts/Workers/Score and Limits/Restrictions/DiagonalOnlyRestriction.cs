using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class DiagonalOnlyRestriction : IRestriction
    {
        private bool _violatedRestriction = false;

        public string GetRestrictionText()
        {
            return "Only diagonal connections";
        }

        public string GetRestrictionDescription() => GetRestrictionText();

        public void Reset()
        {
            _violatedRestriction = false;
        }

        public void SequenceCompleted(LinkedList<ISquarePiece> sequence)
        {
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
                    _violatedRestriction = true;
                    break;
                }
            }
        }

        public void Update(float deltaTime)
        {
            
        }

        public bool ViolatedRestriction()
        {
            return _violatedRestriction;
        }
    }
}
