using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO.Collection;

namespace Assets.Scripts.Workers.IO
{

    public class PieceCollectionIO
    {
        private IPieceCollectionWriter pieceCollectionWriter = new FireBasePieceCollectionWriter();
        static IPieceCollectionReader  pieceCollectionReader = new FireBasePieceCollectionReader();
        
        private static PieceCollectionIO _instance;

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
            ResetData.ResetAllData += ResetSavedData;
        }

        ~PieceCollectionIO()
        {
            ResetData.ResetAllData -= ResetSavedData;
        }

        public void LoadCollectionData()
        {
            pieceCollectionReader.LoadPieceCollectionAsync();
        }

        public void SaveCollectionData()
        {
            pieceCollectionWriter.WritePiecesCollected(PieceCollectionManager.Instance.PiecesCollected);
        }

        private void ResetSavedData()
        {
            PieceCollectionManager.Instance.PiecesCollected.Pieces.Clear();
            SaveCollectionData();
        }
    }
}
