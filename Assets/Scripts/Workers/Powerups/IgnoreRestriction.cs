using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class IgnoreRestriction : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Shield"];

        public void Invoke()
        {
            var keeper = GameObject.FindObjectOfType<ScoreKeeper>();
            keeper.Restriction.Ignore();
        }

        public void Update(float deltaTime)
        {
        }
    }
}
