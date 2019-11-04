using System.Linq;
using Assets.Scripts.Workers.IO.Collection;
using Assets.Scripts.Workers.IO.Data_Entities;
using static SquarePiece;

namespace Assets.Scripts.Workers.Data_Managers
{
    public delegate void PiecesCollectedEvent(Colour type, int previousAmount, int gained);

    public class PieceCollectionManager
    {
        public static PiecesCollectedEvent PiecesCollectedEvent;

        public PiecesCollected PiecesCollected = new PiecesCollected();
        private IPieceCollectionWriter pieceCollectionWriter = new FireBasePieceCollectionWriter();

        private static PieceCollectionManager _instance;

        public static PieceCollectionManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PieceCollectionManager();
                }

                return _instance;
            }
        }

        private PieceCollectionManager()
        {
            PieceSelectionManager.SequenceCompleted += PieceSelectionManager_SequenceCompleted;
            ResetData.ResetAllData += ResetSavedData;
        }

        ~PieceCollectionManager()
        {
            PieceSelectionManager.SequenceCompleted -= PieceSelectionManager_SequenceCompleted;
            ResetData.ResetAllData -= ResetSavedData;
        }

        private void PieceSelectionManager_SequenceCompleted(ISquarePiece[] pieces)
        {
            var grouped = pieces.GroupBy(x => x.PieceColour).Select(group => new { PieceColour = group.Key, Count = group.Count() });
            var previous = 0;
            var totalCollected = 0;

            foreach (var types in grouped)
            {
                var savedCollection = PiecesCollected.Pieces.FirstOrDefault(x => x.PieceColour == types.PieceColour);

                if (savedCollection != null)
                {
                    previous = savedCollection.Count;
                    savedCollection.Count += types.Count;
                    totalCollected = savedCollection.Count;
                }
                else
                {
                    PiecesCollected.Pieces.Add(new PiecesCollected.PieceCollectionInfo() { PieceColour = types.PieceColour, Count = types.Count });
                    totalCollected = types.Count;
                }

                PiecesCollectedEvent?.Invoke(types.PieceColour, previous, types.Count);
            }

            pieceCollectionWriter.WritePiecesCollected(PiecesCollected);
        }

        private void ResetSavedData()
        {
            PiecesCollected.Pieces.Clear();
            pieceCollectionWriter.WritePiecesCollected(PiecesCollected);
        }
    }
}
