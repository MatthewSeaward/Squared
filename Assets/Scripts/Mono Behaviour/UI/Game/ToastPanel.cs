using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ToastPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject Panel;

        [SerializeField]
        private Text text;

        private float displayTime;
        private float timer;

        public static ToastPanel Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            ChildAwake();
        }

        protected virtual void ChildAwake()
        {
        }

        private void Start()
        {
            text = Panel.GetComponentInChildren<Text>();
            Hide();
        }
        public void ShowText(string text)
        {
            ShowText(text, 0);
        }

        public  void ShowText(string text, float displayTime)
        {
            if (!Panel.activeInHierarchy)
            {
                Panel.SetActive(true);
            }

            this.text.text = text;

            if (displayTime > 0)
            {
                Invoke(nameof(Hide), displayTime);
            }
        }

        public void Hide()
        {
            Panel.SetActive(false);
        }
    }
}