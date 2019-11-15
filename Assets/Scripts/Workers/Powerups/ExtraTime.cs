﻿using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using Assets.Scripts.Workers.Score_and_Limits;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class ExtraTime : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Time"];
        
        public string Name => "Extra Time";

        public string Description => "Gives you extra time/turns to complete the level in.";

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

        public void MoveCompleted()
        {
        }

        public void Update(float deltaTime)
        {
        }
    }    
}
