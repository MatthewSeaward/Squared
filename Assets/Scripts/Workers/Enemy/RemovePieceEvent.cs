using Assets.Scripts.Workers.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.Enemy
{
    public class RemovePieceEvent : PositionSelectionRage
    {
        public List<Vector2Int> Positions { get; }

        public RemovePieceEvent(List<Vector2Int> positions)
        {
            this.Positions = positions;
        }
        protected override void InvokeRageActionOnPosition(Vector2Int pos)    
        {
           PieceManager.Instance.RemoveSlot(pos);
        }

        protected override List<Vector2Int> GetSelectedPieces()
        {
            var newArray = new List<Vector2Int>();
            newArray.AddRange(Positions.ToArray());
            return newArray;
        }
    }
}