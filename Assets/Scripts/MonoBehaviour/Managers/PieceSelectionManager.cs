using Assets.Scripts.Managers;
using Assets.Scripts.Workers.IO.Heatmap;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public delegate void SequenceCompleted(LinkedList<ISquarePiece> pieces);

    public class PieceSelectionManager : MonoBehaviour
    {
        public LinkedList<ISquarePiece> CurrentPieces = new LinkedList<ISquarePiece>();
        public Dictionary<Vector2Int, int> UsedPieces = new Dictionary<Vector2Int, int>();

        public static event SequenceCompleted SequenceCompleted;

        public static PieceSelectionManager Instance { private set;  get; }

        private void Awake()
        {
            Instance = this;
            ScoreKeeper.GameCompleted += ScoreKeeper_GameCompleted;
            UsedPieces = new Dictionary<Vector2Int, int>();
        }

        private void OnDestroy()
        {
            ScoreKeeper.GameCompleted -= ScoreKeeper_GameCompleted;
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

            if (GameManager.Instance.GamePaused)
            {
                return;
            }

            if (!Input.GetMouseButtonUp(0))
            {
                return;
            }

            if (CurrentPieces.Count > 1)
            {             

                SequenceCompleted?.Invoke(CurrentPieces);
                LogUsedPieces(CurrentPieces);

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

        private void LogUsedPieces(LinkedList<ISquarePiece> pieces)
        {
            foreach(var piece in pieces)
            {
                if (!UsedPieces.ContainsKey(piece.Position))
                {
                    UsedPieces.Add(piece.Position, 1);
                }
                else
                {
                    UsedPieces[piece.Position]++;
                }
            }
        }

        public bool PieceCanBeRemoved(ISquarePiece piece)
        {
            return (CurrentPieces.Count > 1 && piece == CurrentPieces.Last.Previous.Value);
        }

        public void RemovePiece()
        {
            var lastPiece = CurrentPieces.Last.Value;
            CurrentPieces.RemoveLast();
            lastPiece.Deselected();
        }

        public bool AlreadySelected(ISquarePiece piece)
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

        public void Add(ISquarePiece squarePiece)
        {
            CurrentPieces.AddLast(squarePiece);
        }

        private void ScoreKeeper_GameCompleted(string chapter, int level, int star, int score, GameResult result)
        {
            IHeatmapWriter heatmap = new FirebaseHeatMapWriter();
            heatmap.WriteHeatmapData(chapter, level, UsedPieces);
        }
    }

}
