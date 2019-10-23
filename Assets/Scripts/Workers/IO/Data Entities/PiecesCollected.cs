using System;
using System.Collections.Generic;
using static SquarePiece;

namespace Assets.Scripts.Workers.IO.Data_Entities
{
    [Serializable]
    public class PiecesCollected
    {
        public List<PieceCollectionInfo> Pieces = new List<PieceCollectionInfo>();

        [Serializable]
        public class PieceCollectionInfo
        {
            public Colour PieceColour;
            public int Count;
        }
    }
}