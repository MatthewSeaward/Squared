﻿using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class DoubleMove : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Rune"];

        public string Name => "Double Move";

        public string Description => "Let's you take two moves at once before pieces fall.";

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
