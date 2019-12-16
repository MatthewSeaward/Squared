using Assets.Scripts.Workers.Factorys.Piece_Builder.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects
{
   public class SwapSpriteBehaviour : IBehaviour, ILayeredSprite
    {
        private float timer = 0;

        public Sprite[] GetSprites()
        {
            return new Sprite[] { GameResources.Sprites["Swap"] };
        }

        public void Update(ISquarePiece piece, float deltaTime)
        {
            if (PieceSelectionManager.Instance.AlreadySelected(piece))
            {
                return;
            }

            timer += deltaTime;

            if (timer > Constants.GameSettings.SwapPieceChangeFrequency)
            {
                timer = 0;
                var randomPiece = PieceCreationHelpers.GetRandomSprite();
                while (randomPiece.sprite == piece.Sprite)
                {
                    randomPiece = PieceCreationHelpers.GetRandomSprite();
                }

                piece.Sprite = randomPiece.sprite;
                piece.PieceColour = randomPiece.colour;
            }
        }
    }
}
