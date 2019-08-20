using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
