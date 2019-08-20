using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public delegate void SequenceCompleted(LinkedList<ISquarePiece> pieces);

    public class PieceSelectionManager : MonoBehaviour
    {

        public LinkedList<ISquarePiece> CurrentPieces = new LinkedList<ISquarePiece>();
        public static event SequenceCompleted SequenceCompleted;

        public static PieceSelectionManager Instance { private set;  get; }

        private void Awake()
        {
            Instance = this;
        }

        public ISquarePiece LastPiece
        {
            get
            {
                return CurrentPieces.Last?.Value;
            }
        }

        void Update()
        {
            if (CurrentPieces.Count == 0)
            {
                return;
            }

            if (!Input.GetMouseButtonUp(0))
            {
                return;
            }

            if (CurrentPieces.Count > 1)
            {
                SequenceCompleted(CurrentPieces);

                foreach (var square in CurrentPieces)
                {
                    square.DestroyPiece();
                }
            }
            else
            {
                var firstPiece = CurrentPieces.First.Value;
                firstPiece.Deselected();
            }

            CurrentPieces.Clear();
        }

        public bool PieceCanBeRemoved(SquarePiece piece)
        {
            return (CurrentPieces.Count > 1 && piece == CurrentPieces.Last.Previous.Value);
        }

        public void RemovePiece()
        {
            var lastPiece = CurrentPieces.Last.Value;
            CurrentPieces.RemoveLast();
            lastPiece.Deselected();
        }

        public bool AlreadySelected(SquarePiece piece)
        {
            return CurrentPieces.Contains(piece);
        }

        public void ClearCurrentPieces()
        {
            foreach (var piece in CurrentPieces)
            {
                piece.Deselected();
            }
            CurrentPieces.Clear();
        }

        public void Add(SquarePiece squarePiece)
        {
            CurrentPieces.AddLast(squarePiece);
        }
    }
}
