using Assets.Scripts.Game_Components;
using Assets.Scripts.Managers;
using Assets.Scripts.Workers.IO.Heatmap;
using Assets.Scripts.Workers.Piece_Selection;
using Assets.Scripts.Workers.UserPieceSelection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Constants.GameSettings;

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
        public IUserPieceSelection PieceSelection { get; private set; } = new PieceSelectionDrawLine();

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

            if(CancelButton.MouseOver)
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
            SelectedPiecesChanged?.Invoke(CurrentPieces);
        }

        public void Add(ISquarePiece squarePiece, bool checkForAdditional)
        {
            if (CurrentPieces.Contains(squarePiece))
            {
                return;
            }

            CurrentPieces.AddLast(squarePiece);

            if (checkForAdditional)
            {
                CheckForAdditionalPieces();
            }

            SelectedPiecesChanged?.Invoke(CurrentPieces);
        }

        private void ScoreKeeper_GameCompleted(string chapter, int level, int star, int score, GameResult result)
        {
            IHeatmapWriter heatmap = new FirebaseHeatMapWriter();
            heatmap.WriteHeatmapData(chapter, level, UsedPieces);
        }
    }
}
