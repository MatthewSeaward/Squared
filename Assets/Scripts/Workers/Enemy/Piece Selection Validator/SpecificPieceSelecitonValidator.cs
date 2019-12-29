using System;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection_Validator
{
    class SpecificPieceSelecitonValidator : PieceSelectionValidator
    {
        public PieceTypes specificPiece; 

        public override bool ValidForSelection(ISquarePiece piece)
        {
            return piece.Type == specificPiece;
        }
    }
}
