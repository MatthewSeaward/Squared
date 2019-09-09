using Assets.Scripts.Workers.Piece_Effects.Destruction;
using Assets.Scripts.Workers.Piece_Effects.SwapEffects;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.Enemy
{
    public class PieceSelectionRage : IEnemyRage
    {
        public int SelectionAmount;
        private const float DisplayTime = 1.5f;

        private List<ISquarePiece> selectedPieces = new List<ISquarePiece>();
        private float timer;

        public Vector3 Position { get; set; }

        public void InvokeRage()
        {
            if (MenuProvider.Instance.OnDisplay)
            {
                return;
            }

            int iterationCount = 0;
            while (selectedPieces.Count < SelectionAmount && iterationCount < 100)
            {
                iterationCount++;
                var piece = PieceController.Pieces[Random.Range(0, PieceController.Pieces.Count)];

                if (!ValidForRage(piece))
                {
                    continue;
                }

                if (PieceSelectionManager.Instance.AlreadySelected(piece))
                {
                    PieceSelectionManager.Instance.ClearCurrentPieces();
                }

                if (selectedPieces.Contains(piece))
                {
                    continue;
                }

                selectedPieces.Add(piece);
            }
        }

        public void Update(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= DisplayTime)
            {
                timer = 0f;

                foreach (var piece in selectedPieces)
                {
                    InvokeRageAction(piece);
                }

                selectedPieces.Clear();
                LightningBoltProducer.Instance.ClearBolts();
            }

            foreach(var piece in selectedPieces)
            {
                LightningBoltProducer.Instance.ProduceBolt(piece, piece.transform.position);
            }
        }

        protected virtual void InvokeRageAction(ISquarePiece piece) { }


        private bool ValidForRage(ISquarePiece piece)
        {
            if (piece == null || !piece.gameObject.activeInHierarchy)
            {
                return false;
            }

            if (piece.gameObject.GetComponent<Lerp>() != null && piece.gameObject.GetComponent<Lerp>().LerpInProgress)
            {
                return false;
            }

            if (piece.SwapEffect is LockedSwap)
            {
                return false;
            }

            if (piece.DestroyPieceHandler is DestroyTriggerFall)
            {
                if ((piece.DestroyPieceHandler as DestroyTriggerFall).ToBeDestroyed)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
