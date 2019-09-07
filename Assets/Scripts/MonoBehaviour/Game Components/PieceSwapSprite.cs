using UnityEngine;

namespace Assets.Scripts
{
    public class PieceSwapSprite : MonoBehaviour
    {
        private Sprite newSprite;

        public void SwapSprite(Sprite newSprite)
        {
            this.newSprite = newSprite;
            GetComponent<Animator>().SetTrigger("Sprite Swap");
        }

        public void ChangeSprite()
        {
            GetComponent<SquarePiece>().Sprite = newSprite;
        }
    }
}