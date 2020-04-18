using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;
using static SquarePiece;

namespace Assets.Scripts.Workers.Enemy.Events
{
    public class PiecePercentEventTrigger : EnemyEventTrigger
    {
        public int Percent { private set; get; }
        public Colour Colour { private set;  get; }
        public PieceTypes PieceType { private set; get;}

        public PiecePercentEventTrigger(PieceTypes type, int percent, Colour colour = Colour.None)
        {
            this.PieceType = type;
            this.Percent = percent;
            this.Colour = colour;
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
            GetColour(triggerOn, parts);
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

        private void GetColour(string triggerOn, string[] parts)
        {
            if (parts.Length < 3)
            {
                return;
            }

            if (int.TryParse(parts[2], out int colourInt))
            {
                Colour = (Colour) colourInt;
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
                PieceType = PieceTypes.Empty;
            }

            else if (!EnumHelpers.IsValue<PieceTypes>(type[0]))
            {
                throw new ArgumentException($"Invalid piece type specificed in trigger: {triggerOn}");
            }
            else
            {
                PieceType = (PieceTypes)type[0];
            }
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
            IEnumerable<ISquarePiece> matchingPieces = PieceManager.Instance.Pieces;

            if (PieceType != PieceTypes.Empty)
            {
                matchingPieces = matchingPieces.Where(x => x.Type == PieceType);
            }

            if (Colour != Colour.None)
            {
                matchingPieces = matchingPieces.Where(x => x.PieceColour == Colour);
            }


            var calculatedPercentage = (float)matchingPieces.Count() / (float)PieceManager.Instance.Pieces.Count;
            var percentage = (int)(calculatedPercentage * 100);

            if (percentage >= Percent)
            {
                InvokeRage();
            }
        }
    }
}
