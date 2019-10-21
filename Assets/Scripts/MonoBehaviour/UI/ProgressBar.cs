using Assets.Scripts.Workers.Helpers.Extensions;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private RectTransform innerFill;

        private float totalWidth;
        private float increment;

        private void Start()
        {
            totalWidth = GetComponent<RectTransform>().sizeDelta.x;
            increment = totalWidth / 100f;
        }

        public void UpdateProgressBar(float percent)
        {
            percent = Mathf.Clamp(percent, 0, 100);

            innerFill.SetLeft(totalWidth - (percent * increment));
        }
    }
}
