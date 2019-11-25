using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public delegate void TimerElapsed();

    public class CountdownPanel : ToastPanel
    {
        public static TimerElapsed TimerElapsed;      

        public static new CountdownPanel Instance { get; private set; }

        private float CurrentDuration;
        private float Timer;
  
        protected override void ChildAwake()
        {
            Instance = this;
        }

        private void Update()
        {
            if (CurrentDuration <= 0f)
            {
                return;
            }

            Timer += Time.deltaTime;

            float timeLeft = CurrentDuration - Timer;
            ShowText(timeLeft.ToString("0.00") + " Seconds Remaining");

            if (Timer >= CurrentDuration)
            {
                TimerElapsed?.Invoke();
                Timer = 0f;
                CurrentDuration = 0;
                Hide();
            }
        }

        public void StartCountDown(float duration)
        {
            Timer = 0f;
            CurrentDuration = duration;
        }
    }
}
