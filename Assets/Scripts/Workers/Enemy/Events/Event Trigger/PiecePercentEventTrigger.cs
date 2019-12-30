using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using System;
using System.Linq;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;

namespace Assets.Scripts.Workers.Enemy.Events
{
    public class PiecePercentEventTrigger : EnemyEventTrigger
    {
        public int Percent { private set; get; }
        public PieceTypes PieceType { private set; get;}

        public PiecePercentEventTrigger(PieceTypes type, int percent)
        {
            this.PieceType = type;
            this.Percent = percent;
        }

        public PiecePercentEventTrigger(string triggerOn)
        {
            if (!triggerOn.Contains("-"))
            {
                throw new ArgumentException($"triggerOn in incorrect format. TriggerOn was: {triggerOn}");
            }

            var parts = triggerOn.Split('-');

            GetPieceType(triggerOn, parts);
            GetPercent(triggerOn, parts);

        }

        private void GetPercent(string triggerOn, string[] parts)
        {
            if (int.TryParse(parts[1], out int percent))
            {
                Percent = percent;
            }
            else
            {
                throw new ArgumentException($"Percent in incorrect format. triggerOn was: {triggerOn}");
            }
        }

        private void GetPieceType(string triggerOn, string[] parts)
        {
            var type = parts[0];

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentException($"Piece Type is in incorrect format. TriggerOn was: {triggerOn}");
            }

            if (!EnumHelpers.IsValue<PieceTypes>(type[0]))
            {
                throw new ArgumentException($"Invalid piece type specificed in trigger: {triggerOn}");
            }

            PieceType = (PieceTypes)type[0];
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
            CheckForEvent();
        }

        public void CheckForEvent()
        {
            var matchingPieces = PieceManager.Instance.Pieces.Count(x => x.Type == PieceType);

            var calculatedPercentage = (float)matchingPieces / (float)PieceManager.Instance.Pieces.Count;
            var percentage = (int)(calculatedPercentage * 100);

            if (percentage >= Percent)
            {
                InvokeRage();
            }
        }
    }
}
