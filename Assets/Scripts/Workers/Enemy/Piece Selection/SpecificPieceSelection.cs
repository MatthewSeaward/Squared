using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Managers;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection
{
    class SpecificPieceSelection : IPieceSelection
    {
        public bool CanBeUsed(PieceSelectionValidator validator, int total)
        {
            return PieceManager.Instance.Pieces.Any(x => validator.ValidForSelection(x));
        }

        public List<ISquarePiece> SelectPieces(PieceSelectionValidator validator, int total)
        {
            return PieceManager.Instance.Pieces.Where(x => validator.ValidForSelection(x))
                                         .OrderBy(x => UnityEngine.Random.Range(1, 100))
                                         .Take(total).ToList();

        }
    }
}
