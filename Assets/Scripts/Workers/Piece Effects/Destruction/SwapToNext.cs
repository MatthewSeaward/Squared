using Assets.Scripts.Workers.Factorys.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.Destruction
{
    class SwapToNext : IPieceDestroy, ILayeredSprite
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
            var random =  PieceCreationHelpers.GetRandomSprite();
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

        public Sprite[] GetSprites()
        {
            return new Sprite[] { GameResources.Sprites["Change"], GameResources.Sprites["Padlock"] };
        }
    }
}
