using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts
{
    class SwipeButton : MonoBehaviour, IDragHandler
    {

        public void OnDrag(PointerEventData eventData)
        {
            if (Input.GetAxis("Mouse Y") != 0)
            {
                return;
            }

            var input = Input.GetAxis("Mouse X");
            
            if (input < 0)
            {
                FindObjectOfType<ChapterUIManager>().NextChapter();
            }
            else if (input > 0)
            {
                FindObjectOfType<ChapterUIManager>().PreviousChapter();

            }
        }
    }
}
