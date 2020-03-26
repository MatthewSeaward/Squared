using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.Piece_Connection
{
    public class NoConnection : IPieceConnection
    {
        public List<GameObject> Layers { get; set; }

        public bool ConnectionValidTo(ISquarePiece nextPiece)
        {
            return false;
        }
    }
}
