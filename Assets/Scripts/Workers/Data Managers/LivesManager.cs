using System;
using System.Timers;
using Assets.Scripts.Workers.IO.Data_Entities;
using UnityEngine;

namespace Assets.Scripts.Workers.Data_Managers
{
    public delegate void LivesChanged(bool gained, int newLives);

    public class LivesManager
    {
        public static LivesChanged LivesChanged;

        private Timer Timer;

        private int _livesRemaining;
        private static LivesManager _instance;

        public static LivesManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LivesManager();
                }
                return _instance;
            }
        }

        public DateTime LastEarnedLife { private set; get; } = DateTime.Now;

        public int LivesRemaining
        {
            get
            {
                return Mathf.Clamp(_livesRemaining, 0, 6);
            }
            private set
            {
                value = Mathf.Clamp(value, 0, 6);

                if (value == _livesRemaining)
                {
                    return;
                }

                var increased = value > _livesRemaining;

                _livesRemaining = value;
                UserIO.Instance.SaveLivesInfo();

                LivesChanged?.Invoke(increased, value);
            }
        }

        private LivesManager()
        {
            Timer = new Timer();
            Timer.Elapsed += Timer_Elapsed;
            Timer.Interval = 60000;
            Timer.Start();
        }

        ~LivesManager()
        {
            Timer.Elapsed -= Timer_Elapsed;
            Timer.Dispose();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            WorkOutLivesEarned();
        }

        public void Reset()
        {
            LivesRemaining = 0;
        }

        public void GainALife()
        {
            LivesRemaining++;
        }

        public void LoseALife()
        {
            LivesRemaining--;
        }

        internal void Setup(LivesEntity livesEntity)
        {
            if (livesEntity == null)
            {
                LastEarnedLife = DateTime.Now;
                LivesRemaining = 6;
            }
            else
            {
                LivesRemaining = livesEntity.LivesRemaining;

                if (DateTime.TryParse(livesEntity.LastEarnedLife, out var time))
                {
                    LastEarnedLife = time;
                    WorkOutLivesEarned();
                }
            }
        }

        public void WorkOutLivesEarned(DateTime time)
        {
            LastEarnedLife = time;
            WorkOutLivesEarned();
        }

            public void WorkOutLivesEarned()
        {
            var minutesPast = (DateTime.Now - LastEarnedLife).TotalMinutes;

            int totalEarned = (int) (minutesPast / 10);

            if (totalEarned > 0 || LastEarnedLife == DateTime.MinValue)
            {
                LastEarnedLife = DateTime.Now;
                LivesRemaining += (int)totalEarned;

                UserIO.Instance.SaveLivesInfo();

            }
        }
    }
}
