using UnityEngine;

namespace Assets.Scripts.Workers.Enemy
{
    public class SwapRage : PieceSelectionRage
    {    
        protected override void InvokeRageAction(ISquarePiece piece)
        {
            Sprite newSprite = null;
            do
            {
                newSprite = PieceFactory.Instance.CreateRandomSprite();
            } while (newSprite == piece.Sprite);

            piece.gameObject.GetComponent<PieceSwapSprite>().SwapSprite(newSprite);
        }
    }
}