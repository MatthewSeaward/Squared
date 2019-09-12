using Assets.Scripts.Workers.Helpers.Extensions;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.Destruction
{
    public class SwapSpriteDestroy : IPieceDestroy
    {
        private Sprite _nextPiece;
        private ISwapEffect _swapEffect;
        private SpriteRenderer _innerSprite;
        private ISquarePiece _squarePiece;
        private Vector3 _initialScale;

        public SwapSpriteDestroy(ISwapEffect swapEffect, SpriteRenderer innerSprite, ISquarePiece squarePiece, Vector3 initialScale)
        {
            _swapEffect = swapEffect;
            _innerSprite = innerSprite;
            _squarePiece = squarePiece;
            _initialScale = initialScale;
        }

        public void NotifyOfDestroy()
        {
        }

        public void OnDestroy()
        {
            var colour = _squarePiece.Sprite.texture.GetTextureColour();
            GameResources.PlayEffect("Piece Destroy", _squarePiece.transform.position, colour);

            _squarePiece.Sprite = _nextPiece;
            _squarePiece.Deselected();

            _squarePiece.transform.localScale = _initialScale;
            _squarePiece.SpriteRenderer.color = Color.white;
        }

        public void OnPressed()
        {
            _nextPiece = _swapEffect.ProcessSwap(_squarePiece);
            _innerSprite.gameObject.SetActive(true);
            _innerSprite.sprite = _nextPiece;
        }

        public void Reset()
        {
            _innerSprite.gameObject.SetActive(false);
        }

        public void Update()
        {
        }
    }
}
