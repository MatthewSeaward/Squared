using System.Linq;
using Assets.Scripts.Workers.IO.Collection;
using Assets.Scripts.Workers.IO.Data_Entities;

namespace Assets.Scripts.Workers.Data_Managers
{
    public class PieceCollectionManager
    {
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
            ScoreKeeper.GameCompleted += ScoreKeeper_GameCompleted;
            PieceSelectionManager.SequenceCompleted += PieceSelectionManager_SequenceCompleted;
        }

        ~PieceCollectionManager()
        {
            ScoreKeeper.GameCompleted -= ScoreKeeper_GameCompleted;
            PieceSelectionManager.SequenceCompleted -= PieceSelectionManager_SequenceCompleted;
        }

        private void PieceSelectionManager_SequenceCompleted(ISquarePiece[] pieces)
        {
            var grouped = pieces.GroupBy(x => x.PieceColour).Select(group => new { PieceColour = group.Key, Count = group.Count() });

            foreach (var types in grouped)
            {
                var savedCollection = PiecesCollected.Pieces.FirstOrDefault(x => x.PieceColour == types.PieceColour);

                if (savedCollection != null)
                {
                    savedCollection.Count += types.Count;
                }
                else
                {
                    PiecesCollected.Pieces.Add(new PiecesCollected.PieceCollectionInfo() { PieceColour = types.PieceColour, Count = types.Count });
                }
            }
        }


        private void ScoreKeeper_GameCompleted(string chapter, int level, int star, int score, GameResult result)
        {
            pieceCollectionWriter.WritePiecesCollected(PiecesCollected);
        }
    }
}
