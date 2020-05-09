using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.On_Destroy
{
    class Explosion : IOnDestroy, ILayeredSprite
    {
        private ISquarePiece squarePiece;

        public Explosion(ISquarePiece sqaurePiece)
        {
            this.squarePiece = sqaurePiece;
        }

        public void OnDestroy()
        {
            GameResources.PlayEffect("Explosion", squarePiece.transform.position);
            
            DestroyAdjancent(1, 0);
            DestroyAdjancent(-1, 0);
            DestroyAdjancent(0, 1);
            DestroyAdjancent(0, -1);

            DestroyAdjancent(1, 1);
            DestroyAdjancent(-1, 1);
            DestroyAdjancent(1, -1);
            DestroyAdjancent(-1, -1);
        }

        private void DestroyAdjancent(int x, int y)
        {
            var piece = PieceManager.Instance.GetPiece(squarePiece.Position.x + x, squarePiece.Position.y + y);
            if (piece != null)
            {
                if (piece.DestroyPieceHandler.ToBeDestroyed)
                {
                    return;
                }

                piece.DestroyPiece();
            }
        }

        public LayeredSpriteSettings GetLayeredSprites()
        {
            return new LayeredSpriteSettings() { Sprites = new Sprite[] { GameResources.Sprites["Bomb"] } };
        }
    }
}
