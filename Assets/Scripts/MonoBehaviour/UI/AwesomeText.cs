using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class AwesomeText : MonoBehaviour
    {
        [SerializeField]
        private string[] Words = new string[] { "Awesome", "Fantasic", "Great", "Nice", "Epic", "Fantasic", "Perfect" };

        Animator animator;
        TextMeshProUGUI textMeshPro;
        Image image;

        public void Awake()
        {
            EnemyController.EnemyRaged += ShowText;

            animator = GetComponent<Animator>();
            textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
            image = GetComponent<Image>();

            HideText();
        }
        
        public void OnDestroy()
        {
            EnemyController.EnemyRaged -= ShowText;
        }

        private void ShowText()
        {
            if (MenuProvider.Instance.OnDisplay)
            {
                return;
            }

            SetEnabled(true);
            textMeshPro.SetText(Words[Random.Range(0, Words.Length)]);
            animator.SetTrigger("Show");
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
        }
    }
}
