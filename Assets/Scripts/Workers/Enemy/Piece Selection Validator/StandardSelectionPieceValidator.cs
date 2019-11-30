using Assets.Scripts.Workers.Piece_Effects.Destruction;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection_Validator
{
    class StandardSelectionPieceValidator : PieceSelectionValidator
    {
        public override bool ValidForSelection(ISquarePiece piece)
        {
            if (piece == null || !piece.gameObject.activeInHierarchy)
            {
                return false;
            }

            if (piece.gameObject.GetComponent<Lerp>() != null && piece.gameObject.GetComponent<Lerp>().LerpInProgress)
            {
                return false;
            }

            if (piece.DestroyPieceHandler is LockedSwap)
            {
                return false;
            }

            if (piece.Type == PieceFactory.PieceTypes.Chest)
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
