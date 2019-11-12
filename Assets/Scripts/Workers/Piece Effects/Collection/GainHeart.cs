using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.Collection
{
    public class GainHeart : IOnCollection, ILayeredSprite
    { 
        public void OnCollection()
        {
        }

        public Sprite GetSprite()
        {
            return GameResources.Sprites["Heart"];
        }
    }
}
