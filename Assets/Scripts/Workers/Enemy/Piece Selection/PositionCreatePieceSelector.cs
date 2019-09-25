using System.Collections.Generic;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using UnityEngine;
using static PieceFactory;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection
{
    public class PositionCreatePieceSelector : IPieceSelection
    {
        private readonly List<Vector2Int> Positions;
        private readonly PieceTypes Type;

        public PositionCreatePieceSelector(List<Vector2Int> positions, PieceTypes type)
        {
            this.Positions = positions;
            this.Type = type;
        }

        public List<ISquarePiece> SelectPieces(PieceSelectionValidator validator, int total)
        {
            var selectedPieces = new List<ISquarePiece>();

            foreach (var position in Positions)
            {
                var newPiece = PieceFactory.Instance.CreateSquarePiece(Type, false);

                var squarePiece = newPiece.GetComponent<SquarePiece>();
                squarePiece.Position = position;
                squarePiece.transform.position = new Vector3(PieceController.XPositions[position.x], PieceController.YPositions[position.y]);
                squarePiece.gameObject.SetActive(true);

                selectedPieces.Add(squarePiece);
            }

            return selectedPieces;
        }
    }
}
