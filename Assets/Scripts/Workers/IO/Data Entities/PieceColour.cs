using System;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.Workers.IO.Data_Entities
{
    [Serializable]
    public struct PieceColour 
    {
        [SerializeField]
        public Colour Colour;

        [SerializeField]
        public Sprite Sprite;
    }
}
