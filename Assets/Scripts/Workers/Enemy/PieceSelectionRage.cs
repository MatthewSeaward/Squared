using Assets.Scripts.Workers.Enemy.Piece_Selection;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Workers.Enemy
{
    public abstract class PieceSelectionRage : SelectionRage
    {

        protected abstract PieceSelectionValidator pieceSelectionValidator { get; set; }
        protected abstract IPieceSelection pieceSelection { get; set; }
        private List<ISquarePiece> selectedPieces = new List<ISquarePiece>();

        protected override void PreformSelection()
        {
            selectedPieces = pieceSelection.SelectPieces(pieceSelectionValidator, SelectionAmount);
            selectedPieces.ForEach(x => x.PieceDestroyed += SquarePiece_PieceDestroyed);

            selectedPositions = selectedPieces.Select(x => x.Position).ToList();
        }

        public override bool CanBeUsed()
        {
            return pieceSelection.CanBeUsed(pieceSelectionValidator, SelectionAmount);
        }

        private void SquarePiece_PieceDestroyed(SquarePiece piece)
        {
            piece.PieceDestroyed -= SquarePiece_PieceDestroyed;

            if (selectedPieces.Contains(piece))
            {
                selectedPieces.Remove(piece);
            }

            if (selectedPositions.Contains(piece.Position))
            {
                selectedPositions.Remove(piece.Position);
            }
        }

        protected override void InvokeRageAction()
        {           
            foreach (var piece in selectedPieces)
            {
                PieceSelectionManager.Instance.ClearCurrentPieces();

                InvokeRageActionOnPiece(piece);
            }

            selectedPieces.Clear();
        }

        protected abstract void InvokeRageActionOnPiece(ISquarePiece piece);
    }
}
