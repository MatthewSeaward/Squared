using System;
using System.Collections.Generic;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection
{
    class RandomPieceSelector : IPieceSelection
    {
        public List<ISquarePiece> SelectPieces(PieceSelectionValidator validator, int total)
        {
            var selectedPieces = new List<ISquarePiece>();

            int iterationCount = 0;
            while (selectedPieces.Count < total && iterationCount < 100)
            {
                iterationCount++;
                var piece = PieceController.Pieces[UnityEngine.Random.Range(0, PieceController.Pieces.Count)];

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
