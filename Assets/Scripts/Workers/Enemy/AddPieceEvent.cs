using Assets.Scripts.Workers.Enemy.Piece_Selection;
using System.Collections.Generic;
using UnityEngine;
using static PieceBuilderDirector;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection_Validator
{
    public class AddPieceEvent : PieceSelectionRage
    {
        protected override PieceSelectionValidator pieceSelectionValidator { get; set; } = new PositionValidator();
        protected override IPieceSelection pieceSelection { get; set; }

        public List<Vector2Int> Positions { get; }
        public PieceTypes Type { get; }

        public AddPieceEvent(List<Vector2Int> positions, PieceTypes type)
        {
            this.Positions = positions;
            this.Type = type; 

            pieceSelection = new PositionCreatePieceSelector(positions, type);
        }

        protected override void InvokeRageAction(ISquarePiece piece)
        {
            piece.gameObject.SetActive(true);
            PieceController.AddNewPiece(piece);
        }
    }
}
