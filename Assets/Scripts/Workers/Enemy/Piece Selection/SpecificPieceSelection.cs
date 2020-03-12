using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Managers;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection
{
    class SpecificPieceSelection : IPieceSelection
    {
        private bool _includeQueued;

        public SpecificPieceSelection(bool includeQueued)
        {
            _includeQueued = includeQueued;
        }

        public bool CanBeUsed(PieceSelectionValidator validator, int total)
        {
            return PieceManager.Instance.Pieces.Any(x => validator.ValidForSelection(x));
        }

        public List<ISquarePiece> SelectPieces(PieceSelectionValidator validator, int total)
        {
            var pieces = PieceManager.Instance.Pieces;

            if (_includeQueued)
            {
                pieces.AddRange(PieceDropper.Instance.Pieces.Where(x => validator.ValidForSelection(x)));
            }

            var filteredList = pieces.Where(x => validator.ValidForSelection(x));

            if (total > 0)
            { 
                return filteredList.OrderBy(x => UnityEngine.Random.Range(1, 100))
                                    .Take(total).ToList();

            }

            return filteredList.ToList();
        }
    }
}
