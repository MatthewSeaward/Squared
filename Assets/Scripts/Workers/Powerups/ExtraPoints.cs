using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class ExtraPoints : IPowerup
    {
        public Sprite Icon  => GameResources.Sprites["Trophy"];
        
        public string Name => "Extra Points";

        public string Description => "Increases the amount of points you earn for next turn.";

        public bool Enabled => true;

        public void Invoke()
        {
            throw new NotImplementedException();
        }

        public void Update(float deltaTime)
        {
        }
    }
}
