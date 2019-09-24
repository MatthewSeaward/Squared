using Assets.Scripts.Workers.Enemy.Piece_Selection;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using UnityEngine;

namespace Assets.Scripts.Workers.Enemy
{
    public class SwapRage : PieceSelectionRage
    {
        protected override PieceSelectionValidator pieceSelectionValidator { get; set; } = new StandardSelectionPieceValidator();
        protected override IPieceSelection pieceSelection { get; set; } = new RandomPieceSelector();

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