
using Assets.Scripts.Workers.IO.Data_Entities;
using UnityEngine;

namespace Assets.Scripts.Workers.Enemy
{
    public class SwapRage : PieceSelectionRage
    {       
        public SwapRage(Vector3 Position)
        {
            this.Position = Position;
        }

        protected override void InvokeRageAction(ISquarePiece piece)
        {
            piece.Sprite = PieceFactory.Instance.CreateRandomSprite();
            GameResources.PlayEffect("Piece Destroy", piece.transform.position);
        }
    }
}