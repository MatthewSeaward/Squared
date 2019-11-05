using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class MinePowerup : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Mine"];
        
        public string Name => "Mine";

        public string Description => "Quickly tap on a few pieces to destroy them instantly.";

        public bool Enabled => true;

        public void Invoke()
        {
            throw new System.NotImplementedException();
        }

        public void Update(float deltaTime)
        {
        }
    }
}
