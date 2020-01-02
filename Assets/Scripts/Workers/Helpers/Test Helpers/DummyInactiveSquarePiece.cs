using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.Piece_Effects.Collection;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Helpers.Test_Helpers
{
    class DummyInactiveSquarePiece : ISquarePiece
    {
        public Sprite Sprite { get; set; }

        public Transform transform { get; }

        public Vector2Int Position { get; set; }
        public IPieceConnection PieceConnection { get; set; }
        public IScoreable Scoring { get; set; }
        public IBehaviour PieceBehaviour { get; set; }

        public SpriteRenderer SpriteRenderer { get; }

        public GameObject gameObject { get; }

        public IPieceDestroy DestroyPieceHandler { get; set; }

        public IOnCollection OnCollection { get; }


        public PieceBuilderDirector.PieceTypes Type { get; set; }
        public PieceDestroyed PieceDestroyed { get; set; }
        public SquarePiece.Colour PieceColour { get; set; }

        public bool IsActive => false;

        public void Deselected()
        {
        }

        public void Destroy()
        {
        }

        public void DestroyPiece()
        {
        }

        public void Pressed(bool checkForAdditional)
        {
        }

        public void Selected()
        {
        }

        public void SetMouseDown(bool v)
        {
        }
    }
}
