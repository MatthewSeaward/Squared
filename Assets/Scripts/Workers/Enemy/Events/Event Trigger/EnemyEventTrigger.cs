using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Workers.Enemy.Events
{
    public delegate void EnemyRage();

    public abstract class EnemyEventTrigger
    {
        public IEnemyRage EnemyRage { get; set; }
        public EnemyScript enemy;

        protected bool triggered;

        protected static bool CurrentPlayingTrigger { get; private set; }

        protected void InvokeRage()
        {
            if (GameManager.Instance.GameOver || GameManager.Instance.GameLimit.ReachedLimit())
            {
                return;
            }

            if (triggered)
            {
                return;
            }

            if (CurrentPlayingTrigger)
            {
                return;
            }

            CurrentPlayingTrigger = true;

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
            if (enemy != null)
            {
                enemy.GetComponent<Lerp>().LerpCompleted -= PlayEnemyRage;
                enemy.GetComponent<Animator>().SetTrigger("Angry1");
            }

            EnemyRage.InvokeRage();

            triggered = true;
            CurrentPlayingTrigger = false;
        }

        public virtual void Dispose()
        {
        }
    }
}
