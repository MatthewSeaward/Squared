using System;
using System.Collections.Generic;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Managers;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection
{
    class RandomPieceSelector : IPieceSelection
    {
        public bool CanBeUsed(PieceSelectionValidator validator, int total)
        {
            return true;
        }

        public List<ISquarePiece> SelectPieces(PieceSelectionValidator validator, int total)
        {
            var selectedPieces = new List<ISquarePiece>();

            int iterationCount = 0;
            while (selectedPieces.Count < total && iterationCount < 100)
            {
                iterationCount++;
                var piece = PieceManager.Instance.Pieces[UnityEngine.Random.Range(0, PieceManager.Instance.Pieces.Count)];

                if (!validator.ValidForSelection(piece))
                {
                    continue;
                }

                if (selectedPieces.Contains(piece))
                {
                    continue;
                }

                selectedPieces.Add(piece);
            }

            return selectedPieces;
        }
    }
}
