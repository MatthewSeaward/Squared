using Assets.Scripts.Workers.Helpers.Extensions;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Collections.Generic;
using static PieceFactory;

namespace Assets.Scripts.Workers.Score_and_Limits
{
   public class BannedPieceType : IRestriction
    {
        private bool _violatedRestriction = false;
        private PieceTypes BannedPiece;

        public BannedPieceType(string inputString)
        {
            BannedPiece = (PieceTypes)inputString[0];
        }

        public string GetRestrictionText()
        {
            return $"Banned: {BannedPiece.ToString().Spaced()}";
        }

        public string GetRestrictionDescription() => GetRestrictionText();

        public void Reset()
        {
            _violatedRestriction = false;
        }

        public void SequenceCompleted(LinkedList<ISquarePiece> sequence)
        {
            foreach(var piece in sequence)
            {
                if (piece.Type == BannedPiece)
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
