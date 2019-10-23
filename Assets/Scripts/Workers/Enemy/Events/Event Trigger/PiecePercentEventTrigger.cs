using System.Linq;
using static PieceFactory;

namespace Assets.Scripts.Workers.Enemy.Events
{
    public class PiecePercentEventTrigger : EnemyEventTrigger
    {
        private int percent;
        private PieceTypes type;

        public PiecePercentEventTrigger(PieceTypes type, int percent)
        {
            this.type = type;
            this.percent = percent;
        }

        public override void Start(EnemyScript enemy)
        {
            base.Start(enemy);

            PieceSelectionManager.SequenceCompleted += PieceSelectionManager_SequenceCompleted;
        }

        public override void Dispose()
        {
            PieceSelectionManager.SequenceCompleted -= PieceSelectionManager_SequenceCompleted;
        }

        private void PieceSelectionManager_SequenceCompleted(ISquarePiece[] pieces)
        {
            var matchingPieces = PieceController.Pieces.Count(x => x.Type == type);

            var calculatedPercentage = (float)matchingPieces / (float) PieceController.Pieces.Count;
            var percentage = (int) (calculatedPercentage * 100);

            if (percentage >= percent)
            {
                InvokeRage();
            }
        }
    }
}
