using Assets.Scripts.Workers.Piece_Effects.SwapEffects;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class SwapEffectLimit : IRestriction
    {
        private string effect;
        private bool failed = false;

        public SwapEffectLimit()
        {
            this.effect = typeof(LockedSwap).ToString();
        }

        public string GetDescription()
        {
            return $"Do not use pieces that are Locked";
        }

        public string GetRestrictionText()
        {
            return $"Banned: Locked";
        }

        public bool ViolatedRestriction()
        {
            return failed;
        }

        public void Reset()
        {
            failed = false;
        }

        public void SequenceCompleted(LinkedList<ISquarePiece> sequence)
        {
            foreach (var item in sequence)
            {
                var str1 = item.SwapEffect.GetType().ToString();
                
                if (str1 == effect)
                {
                    failed = true;
                }
            }
        }

        public void Update(float deltaTime)
        {

        }
    }
}
