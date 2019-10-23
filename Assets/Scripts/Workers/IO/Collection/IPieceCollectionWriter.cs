using Assets.Scripts.Workers.IO.Data_Entities;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.IO.Collection
{
    interface IPieceCollectionWriter
    {
        void WritePiecesCollected(PiecesCollected pieces);
    }
}
