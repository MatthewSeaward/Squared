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

        protected void InvokeRage()
        {
            if (triggered)
            {
                return;
            }

            enemy.GetComponent<Lerp>().LerpCompleted += PlayEnemyRage;
            EnemyController.Instance.MoveEnemyOnScreen();
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

            EnemyController.Instance.ShowEnemyText(DialogueManager.Instance.GetAngryText());

            EnemyRage.InvokeRage();

            enemy.GetComponent<Animator>().SetTrigger("Angry1");

            triggered = true;
        }

        public virtual void Dispose()
        {
        }
    }
}
