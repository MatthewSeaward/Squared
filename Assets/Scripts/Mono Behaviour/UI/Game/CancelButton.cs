using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Game_Components
{
    class CancelButton : MonoBehaviour
    {
        [SerializeField]
        private Sprite defaultSprite;

        [SerializeField]
        private Sprite mouseOverSprite;

        public static bool MouseOver;

        [SerializeField]
        
        private void Start()
        {
            PieceSelectionManager.SelectedPiecesChanged += PieceSelectionManager_SelectedPiecesChanged;
            ScoreKeeper.GameCompleted += ScoreKeeper_GameCompleted;

            gameObject.SetActive(false);

            OnMouseExit();
        }

        private void ScoreKeeper_GameCompleted(string chapter, int level, int star, int score, GameResult result, bool dailyChallenge)
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            PieceSelectionManager.SelectedPiecesChanged -= PieceSelectionManager_SelectedPiecesChanged;
            ScoreKeeper.GameCompleted -= ScoreKeeper_GameCompleted;
        }

        private void PieceSelectionManager_SelectedPiecesChanged(System.Collections.Generic.LinkedList<ISquarePiece> pieces)
        {
            gameObject.SetActive(PieceSelectionManager.Instance.CurrentPieces.Count >= 2);
        }

        private void Update()
        {
            if (GameManager.Instance.GamePaused)
            {
                OnMouseExit();
                return;
            }

            if (MenuProvider.Instance.OnDisplay)
            {
                OnMouseExit();
                return;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (MouseOver)
                {
                    PieceSelectionManager.Instance.ClearCurrentPieces();
                    OnMouseExit();
                }
            }
        }

        private void OnMouseEnter()
        {
            GetComponentInChildren<SpriteRenderer>().sprite = mouseOverSprite;
            MouseOver = true;
        }

        private void OnMouseExit()
        {
            GetComponentInChildren<SpriteRenderer>().sprite = defaultSprite;
            MouseOver = false;
        }
    }
}
