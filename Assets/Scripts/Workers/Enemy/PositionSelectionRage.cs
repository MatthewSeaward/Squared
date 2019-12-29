using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.Enemy
{
    public abstract class  PositionSelectionRage : SelectionRage
    {
        public override bool CanBeUsed()
        {
            return GetSelectedPieces().Count > 0;
        }

        protected override void InvokeRageAction()
        {
            foreach(var pos in selectedPositions)
            {
                InvokeRageActionOnPosition(pos);
            }
        }

        protected override void PreformSelection()
        {
            selectedPositions = GetSelectedPieces();
        }

        protected abstract List<Vector2Int> GetSelectedPieces();
        protected abstract void InvokeRageActionOnPosition(Vector2Int position);
    }
}
