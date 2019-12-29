using Assets.Scripts.Workers.Enemy.Piece_Selection;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Factorys.Piece_Builder.Helpers;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.Workers.Enemy
{
    public class SwapRage : PieceSelectionRage
    {
        protected override PieceSelectionValidator pieceSelectionValidator { get; set; } = new StandardSelectionPieceValidator();
        protected override IPieceSelection pieceSelection { get; set; } = new RandomPieceSelector();

        protected override void InvokeRageActionOnPiece(ISquarePiece piece)
        {
            (Sprite Sprite, Colour colour) newSprite;
            do
            {
                newSprite = PieceCreationHelpers.GetRandomSprite();
            }
            while (newSprite.Sprite == piece.Sprite);

            piece.gameObject.GetComponent<PieceSwapSprite>().SwapSprite(newSprite.Sprite, newSprite.colour);
        }
    }
}