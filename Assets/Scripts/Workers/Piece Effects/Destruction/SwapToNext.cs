using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.Destruction
{
    class SwapToNext : IPieceDestroy
    {
        private ISquarePiece piece;

        public bool ToBeDestroyed => false;

        public SwapToNext(ISquarePiece piece)
        {
            this.piece = piece;
        }

        public void NotifyOfDestroy()
        {
        }

        public void OnDestroy()
        {
            var random =  PieceFactory.Instance.CreateRandomSprite();
            piece.PieceColour = random.colour;
            piece.Sprite =  random.sprite;
            piece.Deselected();
        }

        public void OnPressed()
        {
        }  

        public void Reset()
        {
        }

        public void Update()
        {
        }
    }
}
