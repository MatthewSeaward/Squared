using UnityEngine;

namespace Assets.Scripts.Game_Components
{
    class CancelButton : MonoBehaviour
    {
        private Vector3 startPos;

        [SerializeField]
        private Sprite defaultSprite;

        [SerializeField]
        private Sprite mouseOverSprite;

        private bool mouseOver;

        [SerializeField]
        private float maxDistance = 1;

        [SerializeField]
        private float distanceToStart = 3;

        [SerializeField]
        private float distanceToEnd = 0.3f;
        
        private void Start()
        {
            startPos = transform.position;
            OnMouseExit();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                transform.position = startPos;
                GetComponentInChildren<Animator>().SetBool("Enabled", false);

                if (mouseOver)
                {
                    PieceSelectionManager.Instance.ClearCurrentPieces();
                    OnMouseExit();
                }
            }

            if (!Input.GetMouseButton(0) || PieceSelectionManager.Instance.CurrentPieces.Count < 2)
            {
                return;
            }

            GetComponentInChildren<Animator>().SetBool("Enabled", true);

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0);
            if (Vector3.Distance(startPos, mousePos) > distanceToStart)
            {
                transform.position = startPos;
                return;
            }

            if (Vector3.Distance(transform.position, mousePos) < distanceToEnd)
            {
                return;
            }

            transform.position = Vector3.MoveTowards(startPos, mousePos, maxDistance * Time.deltaTime);
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
