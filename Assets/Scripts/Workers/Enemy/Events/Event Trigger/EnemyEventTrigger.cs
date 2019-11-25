using System;
using UnityEngine;

namespace Assets.Scripts.Workers.Enemy.Events
{
    public delegate void EnemyRage();

    public abstract class EnemyEventTrigger
    {
        public IEnemyRage EnemyRage { get; set; }
        public EnemyScript enemy;

        private bool triggered;

        protected static bool CurrentPlayingTrigger { get; private set; }

        protected void InvokeRage()
        {
            if (triggered)
            {
                return;
            }

            if (CurrentPlayingTrigger)
            {
                return;
            }

            CurrentPlayingTrigger = true;

            enemy.GetComponent<Lerp>().LerpCompleted += PlayEnemyRage;
            EnemyController.Instance.ShowEnemyText(DialogueManager.Instance.GetAngryText());
        }

        public virtual void Start(EnemyScript enemy)
        {
            this.enemy = enemy;
            triggered = false;
        }

        internal void Update(float deltaTime)
        {
            EnemyRage.Update(deltaTime);
        }

        private void PlayEnemyRage()
        {
            enemy.GetComponent<Lerp>().LerpCompleted -= PlayEnemyRage;

            EnemyRage.InvokeRage();

            enemy.GetComponent<Animator>().SetTrigger("Angry1");

            triggered = true;
            CurrentPlayingTrigger = false;
        }

        public virtual void Dispose()
        {
        }
    }
}
