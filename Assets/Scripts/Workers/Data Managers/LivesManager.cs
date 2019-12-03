using System;
using Assets.Scripts.Workers.IO.Data_Entities;
using UnityEngine;

namespace Assets.Scripts.Workers.Data_Managers
{
    public delegate void LivesChanged(bool gained, int newLives);

    public static class LivesManager
    {
        public static LivesChanged LivesChanged;
        public static DateTime LastEarnedLife { private set; get; }

        private static int _livesRemaining;
        
        public static int LivesRemaining
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

                LivesChanged?.Invoke(value > _livesRemaining, value);               

                _livesRemaining = value;
                UserIO.Instance.SaveLivesInfo();
            }
        }

        public static void Reset()
        {
            LivesRemaining = 0;
        }

        public static void GainALife()
        {
            LivesRemaining++;
        }

        public static void LoseALife()
        {
            LivesRemaining--;
        }

        internal static void Setup(LivesEntity livesEntity)
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
                    WorkOutLivesEarned(time);
                }
            }
        }

        public static void WorkOutLivesEarned(DateTime lastEarnedLife)
        {
            var minutesPast = (DateTime.Now - lastEarnedLife).TotalMinutes;

            var totalEarned = minutesPast / 10;

            LastEarnedLife = DateTime.Now;
            LivesRemaining += (int) totalEarned;            
        }
    }
}
