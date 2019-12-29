using Assets.Scripts.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.Enemy
{
    public abstract class SelectionRage : IEnemyRage
    {
        public int SelectionAmount;
        private const float DisplayTime = 1.5f;

        
        protected List<Vector2Int> selectedPositions = new List<Vector2Int>();
        private float timer;
        private bool invoked = false;

        public void InvokeRage()
        {
            if (MenuProvider.Instance.OnDisplay)
            {
                return;
            }
            invoked = true;

            PreformSelection();

            GameManager.Instance.ChangePauseState(this, true);

        }

        protected abstract void PreformSelection();

        public abstract bool CanBeUsed();

        public void Update(float deltaTime)
        {
            if (!invoked)
            {
                return;
            }

            timer += deltaTime;
            if (timer >= DisplayTime)
            {
                timer = 0f;

                InvokeRageAction();

                GameManager.Instance.ChangePauseState(this, false);

                selectedPositions.Clear();
                LightningBoltProducer.Instance.ClearBolts();
                invoked = false;
            }

            foreach (var position in selectedPositions)
            {
                LightningBoltProducer.Instance.ProduceBolt(position, PieceController.GetPosition(position));
            }
        }

        protected abstract void InvokeRageAction();
    }
}
