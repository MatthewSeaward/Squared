using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class PaintColour : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Potion"];
        
        public string Name => "Paint Colour";

        public string Description => "Drag your finger over the board to make all pieces the same colour";

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
