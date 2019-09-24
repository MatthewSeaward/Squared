using Assets.Scripts.Workers.Piece_Effects.Destruction;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection_Validator
{
    class AllPiecesConnectionValidator : PieceSelectionValidator
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
