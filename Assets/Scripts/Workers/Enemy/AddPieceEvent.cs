using Assets.Scripts.Workers.Enemy.Piece_Selection;
using System.Collections.Generic;
using UnityEngine;
using static PieceFactory;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection_Validator
{
    class AddPieceEvent : PieceSelectionRage
    {
        protected override PieceSelectionValidator pieceSelectionValidator { get; set; } = new PositionValidator();
        protected override IPieceSelection pieceSelection { get; set; }

        public AddPieceEvent(List<Vector2Int> positions, PieceTypes type)
        {
            pieceSelection = new PositionCreatePieceSelector(positions, type);
        }

        protected override void InvokeRageAction(ISquarePiece piece)
        {
            piece.gameObject.SetActive(true);
            PieceController.AddNewPiece(piece);
        }
    }
}
