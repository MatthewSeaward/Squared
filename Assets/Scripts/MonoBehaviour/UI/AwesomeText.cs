using Assets.Scripts.Constants;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class AwesomeText : MonoBehaviour
    {
        [SerializeField]
        private string[] Words = new string[] { "Awesome", "Fantasic", "Great", "Nice", "Epic", "Fantasic", "Perfect", "Amazing", "Incredible" };

        Animator animator;
        Text text;
        Image image;

        public void Awake()
        {
            PieceSelectionManager.SequenceCompleted += ShowText;
            MenuProvider.MenuDisplayed += HideText;

            animator = GetComponent<Animator>();
            text = GetComponentInChildren<Text>();
            image = GetComponent<Image>();

            HideText();
        }
        
        public void OnDestroy()
        {
            PieceSelectionManager.SequenceCompleted -= ShowText;
            MenuProvider.MenuDisplayed -= HideText;
        }

        private void ShowText(LinkedList<ISquarePiece> pieces)
        {
            if (MenuProvider.Instance.OnDisplay)
            {
                HideText();
                return;
            }

            if (pieces.Count >= GameSettings.AmountForAwesomeText)
            {
                SetEnabled(true);
                text.text = Words[Random.Range(0, Words.Length)];
                animator.SetTrigger("Show");
            }
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
