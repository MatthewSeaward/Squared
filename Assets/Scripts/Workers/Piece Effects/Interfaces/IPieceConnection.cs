using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.Interfaces
{
    public interface IPieceConnection
    {
        List<GameObject> Layers { get; set; }

        bool ConnectionValidTo(ISquarePiece nextPiece);
    }
}
