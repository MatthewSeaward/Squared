using Assets.Scripts.Workers.Core;

namespace Assets.Scripts.Workers.Enemy.Events
{
    class TurnEventTrigger : EnemyEventTrigger
    {
        private Range TurnRange;
        private int TurnsPassed;
        private int CurrentPercentage;
        private int PercentageIncrement;

        public TurnEventTrigger(int min, int max) : this(new Range(min, max))
        {
            
        }

        public TurnEventTrigger(Range turnRange)
        {
            this.TurnRange = turnRange;

            int difference = TurnRange.Max - TurnRange.Min;
            PercentageIncrement = 100 / (difference > 0 ? difference : 1);

       }

        public override void Dispose()
        {
            PieceSelectionManager.SequenceCompleted -= PieceSelectionManager_SequenceCompleted;
        }

        public override void Start(EnemyScript enemy)
        {
            base.Start(enemy);

            TurnsPassed = 0;
            PieceSelectionManager.SequenceCompleted += PieceSelectionManager_SequenceCompleted;
        }

        private void PieceSelectionManager_SequenceCompleted(System.Collections.Generic.LinkedList<ISquarePiece> pieces)
        {
            if (TurnRange.WithinRange(TurnsPassed++))
            {
                CurrentPercentage += PercentageIncrement;

                if (UnityEngine.Random.Range(0, 100) < CurrentPercentage)
                {
                    InvokeRage();
                }
            }
        }
    }
}
