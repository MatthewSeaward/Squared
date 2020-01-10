using Assets.Scripts.Managers;
using Assets.Scripts.Workers.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Workers.Enemy.Events
{
    public delegate void EnemyRage();

    public abstract class EnemyEventTrigger
    {
        public List<IEnemyRage> EnemyRage { get; set; } = new List<IEnemyRage>();
        public EnemyScript enemy;

        protected bool triggered;

        protected static bool CurrentlyPlayingTrigger { get; private set; }
        protected bool RageCanBeUsed => EnemyRage.Any(x => x.CanBeUsed());

        protected void InvokeRage()
        {
            if (!CanPlayRage())
            {
                return;
            }

            CurrentlyPlayingTrigger = true;

            if (enemy != null)
            {
                enemy.GetComponent<Lerp>().LerpCompleted += PlayEnemyRage;
                EnemyController.Instance.ShowEnemyText(DialogueManager.Instance.GetAngryText());
            }
            else
            {
                PlayEnemyRage();
            }
        }

        private bool CanPlayRage()
        {
            if (GameManager.Instance != null)
            {
                if (GameManager.Instance.GameOver)
                {
                    return false;
                }
                if (GameManager.Instance.GameLimit != null && GameManager.Instance.GameLimit.ReachedLimit())
                {
                    return false;
                }
            }

            if (triggered)
            {
                return false;
            }

            if (CurrentlyPlayingTrigger)
            {
                return false;
            }

            return true;
        }

        public virtual void Start(EnemyScript enemy)
        {
            this.enemy = enemy;
            triggered = false;
        }

        internal void Update(float deltaTime)
        {
            EnemyRage.ForEach(x => x.Update(deltaTime));
        }

        private void PlayEnemyRage()
        {
            if (enemy != null)
            {
                enemy.GetComponent<Lerp>().LerpCompleted -= PlayEnemyRage;
                enemy.GetComponent<Animator>().SetTrigger("Angry1");
            }

            EnemyRage.ForEach(x => x.InvokeRage());

            triggered = true;
            CurrentlyPlayingTrigger = false;
        }

        public virtual void Dispose()
        {
        }
    }
}
