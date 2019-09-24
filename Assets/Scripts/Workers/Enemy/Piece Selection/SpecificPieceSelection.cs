using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection
{
    class SpecificPieceSelection : IPieceSelection
    {
        public List<ISquarePiece> SelectPieces(PieceSelectionValidator validator, int total)
        {
            return PieceController.Pieces.Where(x => validator.ValidForSelection(x))
                                         .OrderBy(x => UnityEngine.Random.Range(1, 100))
                                         .Take(total).ToList();

        }
    }
}
