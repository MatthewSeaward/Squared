using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO.Collection;
using Assets.Scripts.Workers.IO.Data_Entities;

namespace Assets.Scripts.Workers.IO
{
    public delegate void PiecesCollectedLoaded();

    public class PieceCollectionIO
    {
        private IPieceCollectionWriter pieceCollectionWriter = new FireBasePieceCollectionWriter();
        static IPieceCollectionReader  pieceCollectionReader = new FireBasePieceCollectionReader();
        
        private static PieceCollectionIO _instance;

        public static PiecesCollectedLoaded PiecesCollectedLoadedEvent;

        public static PieceCollectionIO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PieceCollectionIO();
                }
                return _instance;
          }
        }

        private PieceCollectionIO()
        {
            FireBasePieceCollectionReader.PiecesCollectedLoaded += PiecesCollectedLoaded;
            ResetData.ResetAllData += ResetSavedData;
        }

        ~PieceCollectionIO()
        {
            FireBasePieceCollectionReader.PiecesCollectedLoaded -= PiecesCollectedLoaded;
            ResetData.ResetAllData -= ResetSavedData;
        }

        public void LoadUserData()
        {
            pieceCollectionReader.LoadPieceCollectionAsync();
        }

        public void SaveCollectionData()
        {
            pieceCollectionWriter.WritePiecesCollected(PieceCollectionManager.Instance.PiecesCollected);
        }

        private static void PiecesCollectedLoaded(PiecesCollected piecesCollected)
        {
            PiecesCollectedLoadedEvent?.Invoke();
        }

        private void ResetSavedData()
        {
            PieceCollectionManager.Instance.PiecesCollected.Pieces.Clear();
            SaveCollectionData();
        }
    }
}
