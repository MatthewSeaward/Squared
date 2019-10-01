﻿using Assets.Scripts.Managers;
using Assets.Scripts.Workers.IO.Heatmap;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    public delegate void SequenceCompleted(ISquarePiece[] pieces);
    public delegate void SelectedPiecesChanged(LinkedList<ISquarePiece> pieces);

    public class PieceSelectionManager : MonoBehaviour
    {
        public LinkedList<ISquarePiece> CurrentPieces = new LinkedList<ISquarePiece>();
        public Dictionary<Vector2Int, int> UsedPieces = new Dictionary<Vector2Int, int>();
        private Vector3 lastMousePostion;

        public static event SequenceCompleted SequenceCompleted;
        public static event SelectedPiecesChanged SelectedPiecesChanged;

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

            if (Input.GetMouseButtonUp(0))
            {            

                if (CurrentPieces.Count > 1)
                {
                    CheckForAdditionalPieces();
                    ProcessSequenceCompleted();
                }
                else
                {
                    var firstPiece = CurrentPieces.First.Value;
                    firstPiece.Deselected();
                }

                CurrentPieces.Clear();
                SelectedPiecesChanged?.Invoke(CurrentPieces);
            }

            lastMousePostion = Input.mousePosition;
        }

        private void CheckForAdditionalPieces()
        {
            int inputX = GetAxisDirection("Mouse X");
            int inputY = GetAxisDirection("Mouse Y");     

            var lastPiece = CurrentPieces.Last;

            var additionalSelection = PieceController.Pieces.FirstOrDefault(piece => piece.Position.x == LastPiece.Position.x + inputX &&
                                                                          piece.Position.y == LastPiece.Position.y + inputY);

            if (additionalSelection == null)
            {
                return;
            }

            if (!LastPiece.PieceConnection.ConnectionValid(additionalSelection))
            {
                return;
            }

            CurrentPieces.AddLast(additionalSelection);
        }      

        private int GetAxisDirection(string axis)
        {
            double mouseAxis = Input.GetAxis(axis);

            int result = 0;

            if (mouseAxis != 0)
            {
                result = mouseAxis > 0 ? -1 : 1;
            }

            return result;
        }

        private void ProcessSequenceCompleted()
        {
            SequenceCompleted?.Invoke(CurrentPieces.ToArray());
            LogUsedPieces(CurrentPieces);

            foreach (var square in CurrentPieces)
            {
                square.DestroyPiece();
            }
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
            SelectedPiecesChanged?.Invoke(CurrentPieces);

        }

        public bool AlreadySelected(ISquarePiece piece)        {
            return CurrentPieces.Contains(piece);
        }

        public void ClearCurrentPieces()
        {
            foreach (var piece in CurrentPieces)
            {
                piece.Deselected();
            }
            CurrentPieces.Clear();
            SelectedPiecesChanged?.Invoke(CurrentPieces);
        }

        public void Add(ISquarePiece squarePiece)
        {
            CurrentPieces.AddLast(squarePiece);
            SelectedPiecesChanged?.Invoke(CurrentPieces);
        }

        private void ScoreKeeper_GameCompleted(string chapter, int level, int star, int score, GameResult result)
        {
            IHeatmapWriter heatmap = new FirebaseHeatMapWriter();
            heatmap.WriteHeatmapData(chapter, level, UsedPieces);
        }
    }

}
