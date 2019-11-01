using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using Assets.Scripts.Workers.Score_and_Limits;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class ExtraTime : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Time"];
        public bool Enabled => true;

        public void Invoke()
        {
            var keeper = GameObject.FindObjectOfType<ScoreKeeper>();
            var limit = keeper.GameLimit;

            if (limit is TurnLimit)
            {
                (limit as TurnLimit).Increase(2);
            }
            else if (limit is TimeLimit)
            {
                (limit as TimeLimit).Increase(5);
            }

            keeper.ActivateLimit();
        }

        public void Update(float deltaTime)
        {
        }
    }    
}
