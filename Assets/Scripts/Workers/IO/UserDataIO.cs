using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO.Collection;
using Assets.Scripts.Workers.IO.Data_Entities;

namespace Assets.Scripts.Workers.IO
{
    public delegate void PiecesCollectedLoaded();

    class UserDataIO
    {
        public static PiecesCollectedLoaded PiecesCollectedLoadedEvent;

        static IPieceCollectionReader  pieceCollectionReader = new FireBasePieceCollectionReader();
        
        public static void LoadUserData()
        {
            FireBasePieceCollectionReader.PiecesCollectedLoaded += PiecesCollectedLoaded;

            pieceCollectionReader.LoadPieceCollectionAsync();
        }

        private static void PiecesCollectedLoaded(PiecesCollected piecesCollected)
        {
            FireBasePieceCollectionReader.PiecesCollectedLoaded -= PiecesCollectedLoaded;

            PieceCollectionManager.Instance.PiecesCollected = piecesCollected;

            PiecesCollectedLoadedEvent?.Invoke();
        }
   
    }
}
