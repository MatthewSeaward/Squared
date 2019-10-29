using System.Linq;
using Assets.Scripts.Workers.IO.Collection;
using Assets.Scripts.Workers.IO.Data_Entities;
using static SquarePiece;

namespace Assets.Scripts.Workers.Data_Managers
{
    public delegate void PiecesCollectedEvent(Colour type, int previousAmount, int gained);
    public delegate void PieceCollectionComplete(Colour type);

    public class PieceCollectionManager
    {
        public static PiecesCollectedEvent PiecesCollectedEvent;
        public static PieceCollectionComplete PieceCollectionComplete;

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
        }

        ~PieceCollectionManager()
        {
            PieceSelectionManager.SequenceCompleted -= PieceSelectionManager_SequenceCompleted;
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

                CheckForCompletion(types.PieceColour, totalCollected);

                PiecesCollectedEvent?.Invoke(types.PieceColour, previous, types.Count);
            }

            pieceCollectionWriter.WritePiecesCollected(PiecesCollected);
        }

        private void CheckForCompletion(Colour PieceColour, int totalCollected)
        {
            int increment = RemoteConfigHelper.GetCollectionInterval(PieceColour);

            var multiplier = (totalCollected / increment) + 1;


            if (totalCollected % increment == 0)
            {
                PieceCollectionComplete?.Invoke(PieceColour);
            }
        }
    }
}
