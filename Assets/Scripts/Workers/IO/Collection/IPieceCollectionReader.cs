using System.Threading.Tasks;

namespace Assets.Scripts.Workers.IO.Collection
{
    interface IPieceCollectionReader
    {
        Task LoadPieceCollectionAsync();
    }
}