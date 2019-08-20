using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.Interfaces
{
    public interface ISwapEffect
    {
        Sprite ProcessSwap(ISquarePiece currentPiece);
    }
}
