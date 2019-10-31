using Assets.Scripts.Managers;
using Assets.Scripts.Workers.Enemy.Piece_Selection;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Enemy
{
    public abstract class PieceSelectionRage : IEnemyRage
    {
        public int SelectionAmount;
        private const float DisplayTime = 1.5f;

        protected abstract PieceSelectionValidator pieceSelectionValidator { get; set; }
        protected abstract IPieceSelection pieceSelection { get; set; }

        private List<ISquarePiece> selectedPieces = new List<ISquarePiece>();
        private float timer;

        public void InvokeRage()
        {
            if (MenuProvider.Instance.OnDisplay)
            {
                return;
            }
            GameManager.Instance.GamePaused = true;

            selectedPieces = pieceSelection.SelectPieces(pieceSelectionValidator, SelectionAmount);

            selectedPieces.ForEach(x => x.PieceDestroyed += SquarePiece_PieceDestroyed);
        }

        private void SquarePiece_PieceDestroyed(SquarePiece piece)
        {
            piece.PieceDestroyed -= SquarePiece_PieceDestroyed;

            if (selectedPieces.Contains(piece))
            {
                selectedPieces.Remove(piece);
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
                   PieceSelectionManager.Instance.ClearCurrentPieces();

                    InvokeRageAction(piece);
                    GameManager.Instance.GamePaused = false;
                }

                selectedPieces.Clear();
                LightningBoltProducer.Instance.ClearBolts();
            }

            foreach(var piece in selectedPieces)
            {
                LightningBoltProducer.Instance.ProduceBolt(piece, piece.transform.position);
            }
        }

        protected abstract void InvokeRageAction(ISquarePiece piece);
    }
}
