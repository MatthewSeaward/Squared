using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects
{
   public class SwapSpriteBehaviour : IBehaviour, ILayeredSprite
    {
        private const float SwapFrequency = 5f;
        private float timer = 0;

        public Sprite GetSprite()
        {
            return GameResources.Sprites["Swap"];
        }

        public void Update(SquarePiece piece, float deltaTime)
        {
            if (PieceSelectionManager.Instance.AlreadySelected(piece))
            {
                return;
            }

            timer += deltaTime;

            if (timer > SwapFrequency)
            {
                timer = 0;
                piece.Sprite = PieceFactory.Instance.CreateRandomSprite();
            }
        }
    }
}
