using Assets.Scripts.Workers.Helpers.Extensions;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private RectTransform innerFill;    

        public void UpdateProgressBar(float percent)
        {
            var totalWidth = GetComponent<RectTransform>().sizeDelta.x;
            var increment = totalWidth / 100f;

            percent = Mathf.Clamp(percent, 0, 100);

            innerFill.SetLeft(totalWidth - (percent * increment));
        }
    }
}
