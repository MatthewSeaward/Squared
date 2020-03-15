using Assets.Scripts.Workers.Piece_Effects.Destruction;
using Assets.Scripts.Workers.Test_Mockers.Lerp;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection_Validator
{
    public class AllPiecesConnectionValidator : PieceSelectionValidator
    {
        public override bool ValidForSelection(ISquarePiece piece)
        {
            if (piece == null || !piece.IsActive)
            {
                return false;
            }

            if (piece.gameObject.GetComponent<ILerp>() != null && piece.gameObject.GetComponent<ILerp>().LerpInProgress)
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
