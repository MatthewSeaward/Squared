using Assets.Scripts.Workers.Helpers.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    class ProgressBar : MonoBehaviour
    {
        [SerializeField]
        private RectTransform innerFill;    

        public void UpdateProgressBar(int currentValue, int maxValue, string textToShow = "")
        {
            var calculatedPercent = (float)(currentValue) / (float)maxValue;
            var percent = (int) (calculatedPercent * 100);
            setProgress(percent);

            var Text = GetComponentInChildren<Text>();

            if (Text != null)
            {
                Text.text = textToShow;
            }
        }

        public void UpdateProgressBar(float percent)
        {
            setProgress(percent);
        }

        private void setProgress(float percent)
        {
            var totalWidth = GetComponent<RectTransform>().sizeDelta.x;
            var increment = totalWidth / 100f;

            percent = Mathf.Clamp(percent, 0, 100);

            innerFill.SetLeft(totalWidth - (percent * increment));
        }
    }
}