namespace Assets.Scripts.Workers.Enemy.Events
{
    public class ChangeBoolEvent : IEnemyRage
    {
        public bool Activated { get; private set; }

        public bool CanBeUsed()
        {
            return true;
        }

        public void InvokeRage()
        {
            Activated = true;
        }

        public void Update(float deltaTime)
        {
        }
    }
}
