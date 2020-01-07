using Assets.Scripts.Managers;
using Assets.Scripts.Mono_Behaviour.UI.Components;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Score_and_Limits;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class RestrictionText : MonoBehaviour
    {
        Animator animator;

        [SerializeField]
        Text text;

        [SerializeField]
        Image image;

        [SerializeField]
        Image restrictionImage;

        public void Start()
        {
            animator = GetComponent<Animator>();
   
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
            GetComponentInChildren<RestrictionInfo>().DisplayRestriction(currentRestriction);

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

            GameManager.Instance.ChangePauseState(this, enabled);
        }
    }
}
