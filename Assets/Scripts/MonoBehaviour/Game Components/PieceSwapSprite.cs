using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts
{
    public class PieceSwapSprite : MonoBehaviour
    {
        private Sprite newSprite;
        private Colour newColour;

        public void SwapSprite(Sprite Sprite, Colour colour)
        {
            this.newSprite = Sprite;
            this.newColour = colour;
            GetComponent<Animator>().SetTrigger("Sprite Swap");
        }

        public void ChangeSprite()
        {
            GetComponent<SquarePiece>().Sprite = newSprite;
            GetComponent<SquarePiece>().PieceColour = newColour;
        }
    }
}