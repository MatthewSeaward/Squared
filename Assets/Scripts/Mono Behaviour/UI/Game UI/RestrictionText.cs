using Assets.Scripts.Workers.Score_and_Limits;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class RestrictionText : MonoBehaviour
    {
        Animator animator;
        Text text;
        Image image;

        public void Awake()
        {

            animator = GetComponent<Animator>();
            text = GetComponentInChildren<Text>();
            image = GetComponent<Image>();

            HideText();

            LevelStart.GameStarted += ShowText;
        }

        public void OnDestroy()
        {
            LevelStart.GameStarted -= ShowText;
        }

        public void ShowText()
        {
            var currentRestriction = LevelManager.Instance.SelectedLevel.GetCurrentRestriction();
            if (currentRestriction is NoRestriction)
            {
                return;
            }
            
            SetEnabled(true);
            text.text = currentRestriction.GetRestrictionDescription();;
            animator.SetTrigger("Show Restriction");
        }

        public void HideText()
        {
            SetEnabled(false);
        }

        private void SetEnabled(bool enabled)
        {
            image.enabled = enabled;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(enabled);
            }
            animator.enabled = enabled;
        }
    }
}
