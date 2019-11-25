﻿using Assets.Scripts.Workers.Core;

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
            PieceSelectionManager.MoveCompleted -= PieceSelectionManager_MoveCompleted;
        }

        public override void Start(EnemyScript enemy)
        {
            base.Start(enemy);

            TurnsPassed = 0;
            PieceSelectionManager.MoveCompleted += PieceSelectionManager_MoveCompleted;
        }

        private void PieceSelectionManager_MoveCompleted()
        {
            if (TurnRange.WithinRange(TurnsPassed++) && !CurrentPlayingTrigger && EnemyRage.CanBeUsed())
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
