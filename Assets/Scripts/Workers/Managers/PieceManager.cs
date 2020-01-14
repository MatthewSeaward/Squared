using Assets.Scripts.Workers.Piece_Effects.Destruction;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Workers.Managers
{
    public class PieceManager
    {
        public List<ISquarePiece> Pieces { get; private set; }
        public float[] YPositions { get; private set; }
        public float[] XPositions { get; private set; }

        private List<Vector2Int> AvaiableSlots = new List<Vector2Int>();

        public int NumberOfRows => YPositions.Length;
        public int NumberOfColumns => XPositions.Length;

        private static PieceManager _instance;

        public static PieceManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PieceManager();
                }
                return _instance;
            }
        }

        private PieceManager()
        {

        }

        public bool HasEmptySlotInColumn(int x, out int y)
        {
            for (int i = 0; i < NumberOfRows; i++)
            {
                if (IsEmptySlot(x, i))
                {
                    y = i;
                    return true;
                }
            }

            y = 0;
            return false;
        }

        public void Setup(List<ISquarePiece> Pieces, float[] XPositions, float[] YPositions)
        {
            this.Pieces = Pieces;
            this.YPositions = YPositions;
            this.XPositions = XPositions;

            AvaiableSlots.Clear();
            foreach (var piece in Pieces)
            {
                AvaiableSlots.Add(piece.Position);
            }
        }

        public void AddNewPiece(ISquarePiece newPiece)
        {
            Pieces.Add(newPiece);
            AvaiableSlots.Add(newPiece.Position);
        }

        public void RemoveSlot(Vector2Int position)
        {
            var piece = GetPiece(position.x, position.y);
            if (piece != null)
            {
                piece.DestroyPiece();
                piece?.gameObject?.SetActive(false);
            }

            AvaiableSlots.Remove(position);
        }

        public bool SlotExists(Vector2Int position)
        {
            return AvaiableSlots.Any(x => x == position);
        }

        public bool IsEmptySlot(int x, int y)
        {
            if (!AvaiableSlots.Contains(new Vector2Int(x, y)))
            {
                return false;
            }

            var currentPiece = GetPiece(x, y);
            if (currentPiece == null || !currentPiece.IsActive)
            {
                return true;
            }

            if (currentPiece.DestroyPieceHandler is LockedSwap)
            {
                return IsEmptySlot(x, y + 1);
            }

            return false;
        }

        public Vector3 GetPosition(Vector2Int position)
        {
            return new Vector3(XPositions[position.x], YPositions[position.y]);
        }

        public bool HasSlotInColumn(int column)
        {
            for (int row = 0; row < NumberOfRows; row++)
            {
                if (!AvaiableSlots.Contains(new Vector2Int(row, column)))
                {
                    return false;
                }
            }
            return true;
        }
        
        public ISquarePiece GetPiece(int x, int y)
        {
            return Pieces.FirstOrDefault(p => p.Position.x == x && p.Position.y == y);
        }

        public ISquarePiece[] GetPiecesAbove(int x, int y)
        {
            return Pieces.Where(p => p.Position.x == x && ValidXPosition(p.Position.x)
                                  && p.Position.y < y && ValidYPosition(p.Position.y)
                                  && SlotExists(p.Position)).ToArray();
        }

        private bool ValidXPosition(int x) => x >= 0 && x < NumberOfRows;

        private bool ValidYPosition(int y) => y >= 0 && y < NumberOfColumns;    
    }
}