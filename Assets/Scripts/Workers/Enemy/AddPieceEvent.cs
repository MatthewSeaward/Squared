using Assets.Scripts.Workers.Managers;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;

namespace Assets.Scripts.Workers.Enemy.Piece_Selection_Validator
{
    public class AddPieceEvent : PositionSelectionRage
    {
        public List<Vector2Int> Positions { get; }
        public PieceTypes Type { get; }

        public AddPieceEvent(List<Vector2Int> positions, PieceTypes type)
        {
            this.Positions = positions;
            this.Type = type; 
        }             

        protected override List<Vector2Int> GetSelectedPieces()
        {
            var newArray = new List<Vector2Int>();
            newArray.AddRange(Positions.ToArray());
            return newArray;
        }

        protected override void InvokeRageActionOnPosition(Vector2Int position)
        {
            var newPiece = Instance.CreateSquarePiece(Type, false);

            var squarePiece = newPiece.GetComponent<SquarePiece>();
            squarePiece.Position = position;
            squarePiece.transform.position = new Vector3(PieceManager.Instance.XPositions[position.x], PieceManager.Instance.YPositions[position.y]);
            squarePiece.gameObject.SetActive(true);

            PieceManager.Instance.AddNewPiece(squarePiece);
        }
    }
}