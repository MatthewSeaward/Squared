using System;
using Assets.Scripts.Workers.IO.Data_Entities;
using UnityEngine;

namespace Assets.Scripts.Workers.Data_Managers
{
    public delegate void LivesChanged(bool gained, int newLives);

    public static class LivesManager
    {
        public static LivesChanged LivesChanged;

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
            LivesRemaining = livesEntity.LivesRemaining;
        }
    }
}
