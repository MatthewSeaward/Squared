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
        private Vector3 lastPos;

        private void Start()
        {
            startPos = transform.position;
            lastPos = startPos;
            OnMouseExit();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                transform.position = startPos;
                GetComponent<Animator>().SetBool("Enabled", false);

                if (mouseOver)
                {
                    PieceSelectionManager.Instance.ClearCurrentPieces();
                    OnMouseExit();
                }
            }

            if (!Input.GetMouseButton(0) || PieceSelectionManager.Instance.CurrentPieces.Count == 0)
            {
                return;
            }

            GetComponent<Animator>().SetBool("Enabled", true);

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float moveAmount = Mathf.Clamp(Vector3.Distance(mousePos, startPos) - maxDistance, 0, maxDistance);
            transform.position = Vector3.MoveTowards(transform.position, mousePos, maxDistance);            

            if (Vector3.Distance(startPos, transform.position) > maxDistance)
            {
                transform.position = lastPos;
            }

            lastPos = transform.position;

        }

        private void OnMouseEnter()
        {
            GetComponent<SpriteRenderer>().sprite = mouseOverSprite;
            mouseOver = true;
        }

        private void OnMouseExit()
        {
            GetComponent<SpriteRenderer>().sprite = defaultSprite;
            mouseOver = false;
        }
    }
}
