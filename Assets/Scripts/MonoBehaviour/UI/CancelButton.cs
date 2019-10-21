using UnityEngine;

namespace Assets.Scripts.Game_Components
{
    class CancelButton : MonoBehaviour
    {
        [SerializeField]
        private Sprite defaultSprite;

        [SerializeField]
        private Sprite mouseOverSprite;

        private bool mouseOver;

        [SerializeField]
        
        private void Start()
        {
            PieceSelectionManager.SelectedPiecesChanged += PieceSelectionManager_SelectedPiecesChanged;
            OnMouseExit();
        }

        private void OnDestroy()
        {
            PieceSelectionManager.SelectedPiecesChanged -= PieceSelectionManager_SelectedPiecesChanged;
        }

        private void PieceSelectionManager_SelectedPiecesChanged(System.Collections.Generic.LinkedList<ISquarePiece> pieces)
        {
            GetComponentInChildren<Animator>().SetBool("Enabled", Input.GetMouseButton(0) && PieceSelectionManager.Instance.CurrentPieces.Count >= 2);
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (mouseOver)
                {
                    PieceSelectionManager.Instance.ClearCurrentPieces();
                    OnMouseExit();
                }
            }

            //if (!Input.GetMouseButton(0) || PieceSelectionManager.Instance.CurrentPieces.Count < 2)
            //{
            //    return;
            //}

        }

        private void OnMouseEnter()
        {
            GetComponentInChildren<SpriteRenderer>().sprite = mouseOverSprite;
            mouseOver = true;
        }

        private void OnMouseExit()
        {
            GetComponentInChildren<SpriteRenderer>().sprite = defaultSprite;
            mouseOver = false;
        }
    }
}
