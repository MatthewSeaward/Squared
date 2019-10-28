using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class PerformBestMove : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Crown"];
    }
}
