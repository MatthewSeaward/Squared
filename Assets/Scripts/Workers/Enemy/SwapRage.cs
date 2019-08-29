
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Destruction;
using Assets.Scripts.Workers.Piece_Effects.SwapEffects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.Enemy
{
    public class SwapRage : IEnemyRage
    {
        public int AmountToSwap = 4;
        private const float DisplayTime = 0.5f;

        private Dictionary<Vector2Int, Vector2> lines = new Dictionary<Vector2Int, Vector2>();
        private float timer;

        public SwapRage(Vector3 Position)
        {
            this.Position = Position;
        }

        public Vector3 Position { get; }

        public void InvokeRage()
        {
            for (int i = 0; i < AmountToSwap; i++)
            {
                var piece = PieceController.Pieces[Random.Range(0, PieceController.Pieces.Count)];

               if (!ValidDestruction(piece))
               {
                  continue;
               }

                if (PieceSelectionManager.Instance.AlreadySelected(piece))
                {
                    PieceSelectionManager.Instance.ClearCurrentPieces();
                }

                if (lines.ContainsKey(piece.Position))
                {
                    continue;
                }

                lines.Add(piece.Position, new Vector2(piece.transform.position.x, piece.transform.position.y));
                piece.Sprite = PieceFactory.Instance.CreateRandomSprite();
                GameResources.PlayEffect("Piece Destroy", piece.transform.position);
            }
        }

        public void Update(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= DisplayTime)
            {
                timer = 0f;
                lines.Clear();
            }

            foreach (var line in lines.Values)
            {
                LineFactory.Instance.GetLine(new Vector2(Position.x, Position.y), line, 0.02f, Color.blue);
            }
        }

        private bool ValidDestruction(ISquarePiece piece)
        {
            if (piece == null || !piece.gameObject.activeInHierarchy)
            {
                return false;
            }
            if (piece.SwapEffect is LockedSwap)
            {
                return false;
            }

            if (piece.DestroyPieceHandler is DestroyTriggerFall)
            {
                if ((piece.DestroyPieceHandler as DestroyTriggerFall).TobeDestroyed)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
