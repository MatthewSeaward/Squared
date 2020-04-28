using Assets.Scripts.Game_Components;
using Assets.Scripts.Managers;
using Assets.Scripts.Workers.IO.Heatmap;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Piece_Selection;
using Assets.Scripts.Workers.UserPieceSelection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Constants.GameSettings;

namespace Assets.Scripts
{
    public delegate void SequenceCompleted(ISquarePiece[] pieces);
    public delegate void MoveCompleted();
    public delegate void SelectedPiecesChanged(LinkedList<ISquarePiece> pieces);

    public class PieceSelectionManager : MonoBehaviour
    {
        public LinkedList<ISquarePiece> CurrentPieces = new LinkedList<ISquarePiece>();
        public Dictionary<Vector2Int, int> UsedPieces = new Dictionary<Vector2Int, int>();
        private Vector3 lastMousePostion;

        private const int DefaultMovesPerTurn = 1;
        private int MovesAllowedPerTurn;

        public static event SequenceCompleted SequenceCompleted;
        public static event SelectedPiecesChanged SelectedPiecesChanged;
        public static event MoveCompleted MoveCompleted;

        private IPieceSelectionMode DefaultSelectionMode = new PieceSelectionModeDrawLine();
        public IPieceSelectionMode PieceSelection { get; set; }
        public List<List<ISquarePiece>> StoredMoves { private set; get; } = new List<List<ISquarePiece>>();

        public static PieceSelectionManager Instance { private set;  get; }
        
        public void Awake()
        {
            Instance = this;
            ScoreKeeper.GameCompleted += ScoreKeeper_GameCompleted;
            UsedPieces = new Dictionary<Vector2Int, int>();
            ReturnPieceSelectionModeToDefault();

            ResetMovesAllowedPerTurn();
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

            if(CancelButton.MouseOver)
            {
                return;
            }

            if (MenuProvider.Instance.OnDisplay)
            {
                return;
            }

            if (!Input.GetMouseButton(0))
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

        public void PreformMove(List<ISquarePiece> pieces)
        {
            foreach (var piece in pieces)
            {
                CurrentPieces.AddLast(piece);
            }

            ProcessSequenceCompleted();
            SelectedPiecesChanged?.Invoke(CurrentPieces);
        }

        private void CheckForAdditionalPieces()
        {
            int inputX = GetAxisDirection("Mouse X");
            int inputY = GetAxisDirection("Mouse Y");     

            var lastPiece = CurrentPieces.Last;

            var additionalSelection = PieceManager.Instance.Pieces.FirstOrDefault(piece => piece.Position.x == LastPiece.Position.x + inputX &&
                                                                          piece.Position.y == LastPiece.Position.y - inputY);

            if (additionalSelection == null)
            {
                return;
            }

            var newSelected = new List<ISquarePiece>();
            newSelected.AddRange(CurrentPieces);
            newSelected.Add(additionalSelection);

            if (LevelManager.Instance.SelectedLevel.GetCurrentRestriction().IsRestrictionViolated(newSelected.ToArray()))
            {
                return;
            }

            additionalSelection.Pressed(false);
        }

        private int GetAxisDirection(string axis)
        {
            double mouseAxis = Input.GetAxis(axis);

            if (mouseAxis > AdditionalSquareSensitvity)
            {
                return 1;
            }
            else if (mouseAxis < -AdditionalSquareSensitvity)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        private void ProcessSequenceCompleted()
        {
            StoredMoves.Add(CurrentPieces.Select(x => x).ToList());

            if (StoredMoves.Count >= MovesAllowedPerTurn)
            {
                foreach (var currentMoves in StoredMoves)
                {
                    SequenceCompleted?.Invoke(currentMoves.ToArray());
                    LogUsedPieces(currentMoves);

                    foreach (var square in currentMoves)
                    {
                        if (square.OnCollection != null)
                        {
                            square.OnCollection.OnCollection();
                        }
                        square.DestroyPiece();
                    }
                }
                MoveCompleted?.Invoke();

                StoredMoves.Clear();
                ResetMovesAllowedPerTurn();
            }

            CurrentPieces.Clear();
            SelectedPiecesChanged?.Invoke(CurrentPieces);
        }

        private void LogUsedPieces(List<ISquarePiece> pieces)
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
            return CurrentPieces.Count > 1 && CurrentPieces.Contains(piece);
        }

        public void RemovePiece(ISquarePiece piece)
        {
            var lastPiece = CurrentPieces.Last.Value;

            while (lastPiece != piece)
            {
                RemoveLastPiece(lastPiece);
                lastPiece = CurrentPieces.Last.Value;
            }

            SelectedPiecesChanged?.Invoke(CurrentPieces);
        }

        private void RemoveLastPiece(ISquarePiece lastPiece)
        {
            CurrentPieces.RemoveLast();
            lastPiece.Deselected();
        }

        public bool AlreadySelected(ISquarePiece piece)
        {
            if (CurrentPieces.Contains(piece))
            {
                return true;
            }

            foreach(var move in StoredMoves)
            {
                if (move.Contains(piece))
                {
                    return true;
                }
            }

            return false;
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

        public void Add(ISquarePiece squarePiece, bool checkForAdditional)
        {
            if (CurrentPieces.Contains(squarePiece))
            {
                return;
            }

            CurrentPieces.AddLast(squarePiece);

            if (CurrentPieces.Count == 1 & checkForAdditional)
            {
               CheckForAdditionalPieces();
            }

            SelectedPiecesChanged?.Invoke(CurrentPieces);
        }

        private void ScoreKeeper_GameCompleted(string chapter, int level, int star, int score, GameResult result, bool dailyChallenge)
        {
            IHeatmapWriter heatmap = new FirebaseHeatMapWriter();
            heatmap.WriteHeatmapData(chapter, level, UsedPieces);
        }

        public void ReturnPieceSelectionModeToDefault()
        {
            PieceSelection = DefaultSelectionMode;
        }

        public void ResetMovesAllowedPerTurn()
        {
            MovesAllowedPerTurn = DefaultMovesPerTurn;
        }

        public void ChangeMovesAllowedPerTurn(int movesPerTurn)
        {
            MovesAllowedPerTurn = movesPerTurn;
        }
    }
}
