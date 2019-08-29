using UnityEngine;

namespace Assets.Scripts.Workers.Enemy
{
    public class DestroyRage : PieceSelectionRage
    {
        public DestroyRage(Vector3 Position)
        {
            this.Position = Position;
        }

        protected override void InvokeRageAction(ISquarePiece piece)
        {
            piece.DestroyPiece();
        }
    }
}
