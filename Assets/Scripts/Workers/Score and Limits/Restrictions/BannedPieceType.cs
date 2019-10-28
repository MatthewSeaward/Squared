using Assets.Scripts.Workers.Helpers.Extensions;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using static PieceFactory;

namespace Assets.Scripts.Workers.Score_and_Limits
{
   public class BannedPieceType : IRestriction
    {
        private bool _violatedRestriction = false;
        private PieceTypes BannedPiece;
        private bool ignored;

        public BannedPieceType(string inputString)
        {
            BannedPiece = (PieceTypes)inputString[0];
        }

        public string GetRestrictionText()
        {
            return "Banned: " + BannedPiece.ToString().Spaced();
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

            foreach (var piece in sequence)
            {
                if (piece.Type == BannedPiece)
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
