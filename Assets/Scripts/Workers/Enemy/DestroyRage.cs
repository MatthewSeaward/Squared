using UnityEngine;

namespace Assets.Scripts.Workers.Enemy
{
    public class DestroyRage : PieceSelectionRage
    {
      
        protected override void InvokeRageAction(ISquarePiece piece)
        {
            piece.DestroyPiece();
        }
    }
}
