using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public delegate void TimerElapsed();

    public class CountdownPanel : MonoBehaviour
    {
        public static TimerElapsed TimerElapsed;

        [SerializeField]
        private GameObject Panel;

        [SerializeField]
        private Text text;

        public static CountdownPanel Instance { get; private set; }

        private float CurrentDuration;
        private float Timer;

        private void Awake()
        {
            Instance = this;           
        }

        private void Start()
        {
            text = Panel.GetComponentInChildren<Text>();
            Panel.SetActive(false);
        }

        private void Update()
        {
            if (CurrentDuration <= 0f)
            {
                return;
            }

            Timer += Time.deltaTime;

            float timeLeft = CurrentDuration - Timer;
            text.text = timeLeft.ToString("0.00") + " Seconds Remaining";

            if (Timer >= CurrentDuration)
            {
                TimerElapsed?.Invoke();
                Timer = 0f;
                CurrentDuration = 0;
                Panel.SetActive(false);
            }
        }

        public void StartCountDown(float duration)
        {
            Timer = 0f;
            CurrentDuration = duration;
            Panel.SetActive(true);
        }
    }
}
