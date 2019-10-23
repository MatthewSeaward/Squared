using System;
using UnityEngine;
using static SquarePiece;

namespace DataEntities
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
