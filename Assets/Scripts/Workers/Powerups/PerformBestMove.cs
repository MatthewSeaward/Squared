using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class PerformBestMove : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Crown"];
        
        public string Name => "Best Move";

        public string Description => "Instantly preforms the best move.";

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
