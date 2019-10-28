using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class ExtraLife : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Extra Life"];

        public void Invoke()
        {
            throw new System.NotImplementedException();
        }

        public void Update(float deltaTime)
        {
        }
    }
}
