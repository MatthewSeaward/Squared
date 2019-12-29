using Assets.Scripts.Workers.IO.Data_Entities;

namespace Assets.Scripts.Workers.IO.Collection
{
    interface IPieceCollectionWriter
    {
        void WritePiecesCollected(PiecesCollected pieces);
    }
}
