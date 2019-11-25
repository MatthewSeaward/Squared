using Assets.Scripts.Workers.Piece_Effects.Destruction;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class SwapEffectLimit : IRestriction
    {
        public readonly string effect;
        private bool failed = false;
        public bool Ignored { get; private set; }

        public SwapEffectLimit()
        {
            this.effect = typeof(LockedSwap).ToString();
        }

        public string GetRestrictionDescription() => GetRestrictionText();

        public string GetRestrictionText()
        {
            return "Banned: Locked";
        }

        public bool ViolatedRestriction()
        {
            return failed;
        }

        public void Reset()
        {
            failed = false;
        }

        public void SequenceCompleted(ISquarePiece[] sequence)
        {
            failed = IsRestrictionViolated(sequence);
            Ignored = false;
        }

        public void Update(float deltaTime)
        {

        }

        public bool IsRestrictionViolated(ISquarePiece[] sequence)
        {
            if (Ignored)
            {
                return false;
            }

            foreach(var item in sequence)
            {
                var str1 = item.DestroyPieceHandler.GetType().ToString();

                if (str1 == effect)
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
