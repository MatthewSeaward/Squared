using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection
{
    public interface IPieceSelection
    {
        List<ISquarePiece> SelectPieces(PieceSelectionValidator validator, int total);
    }
}
