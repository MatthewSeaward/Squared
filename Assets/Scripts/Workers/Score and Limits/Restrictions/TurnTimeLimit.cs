using Assets.Scripts.Managers;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    class TurnTimeLimit : IRestriction
    {
        private float Limit;
        private float TimeTaken;
        private float TimeLeft => Mathf.Clamp(Limit - TimeTaken, 0, Limit);

        public TurnTimeLimit(float limit)
        {
            this.Limit = limit;
        }

        public string GetRestrictionText()
        {
            return "Time per turn: " + TimeLeft.ToString("0.00");
        }

        public string GetRestrictionDescription()
        {
            return $"Time per turn: {Limit.ToString("0")} seconds";
        }

        public void Reset()
        {
            TimeTaken = 0;
        }

        public void SequenceCompleted(ISquarePiece[] sequence)
        {
            TimeTaken = 0;
        }

        public void Update(float deltaTime)
        {
            if (GameManager.Instance.GamePaused || MenuProvider.Instance.OnDisplay)
            {
                return;
            }

            TimeTaken += deltaTime;
        }

        public bool ViolatedRestriction()
        {
            return TimeLeft <= 0;
        }

        public bool IsRestrictionViolated(ISquarePiece[] sequence)
        {
            return false;
        }
    }
}