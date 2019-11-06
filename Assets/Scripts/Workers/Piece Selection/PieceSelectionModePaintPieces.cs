using Assets.Scripts.Workers.Helpers.Extensions;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.UserPieceSelection;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Selection
{
    public class PieceSelectionModePaintPieces : IPieceSelectionMode
    {
        private Sprite ChosenSprite;

        public void Piece_MouseDown(ISquarePiece piece, bool checkForAdditional)
        {
            if (piece.Type == PieceFactory.PieceTypes.Rainbow)
            {
                return;
            }

            ChosenSprite = piece.Sprite;
        }

        public void Piece_MouseEnter(ISquarePiece piece)
        {
            if (ChosenSprite == null)
            {
                Piece_MouseDown(piece, false);
                return;
            }

            if (piece.Type == PieceFactory.PieceTypes.Rainbow)
            {
                return;
            }

            var colour = ChosenSprite.texture.GetTextureColour();
            GameResources.PlayEffect("Piece Destroy", piece.transform.position, colour);

            piece.Sprite = ChosenSprite;
        }

        public void Piece_MouseUp(ISquarePiece piece)
        {
        }
    }
}