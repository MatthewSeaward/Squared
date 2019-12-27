using Assets.Scripts.Workers.Enemy.Piece_Selection;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Helpers.Extensions;
using Assets.Scripts.Workers.IO.Data_Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.Enemy
{
    public class RemovePieceEvent : PieceSelectionRage
    {
        protected override PieceSelectionValidator pieceSelectionValidator { get; set; } = null;
        protected override IPieceSelection pieceSelection { get; set; }

        public List<Vector2Int> Positions { get; }

        public RemovePieceEvent(List<Vector2Int> positions)
        {
            this.Positions = positions;

            pieceSelection = new SpecificPositionSelector(positions);
        }

        protected override void InvokeRageAction(ISquarePiece piece)
        {
            var colour = piece.Sprite.texture.GetTextureColour();
            GameResources.PlayEffect("Piece Destroy", piece.transform.position, colour);

            piece.gameObject.SetActive(false);
            PieceController.RemovePiece(piece);

        }
    }
}
