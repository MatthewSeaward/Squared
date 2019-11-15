namespace Assets.Scripts.Workers.Score_and_Limits.Interfaces
{
    public interface IGameLimit
    {
        void Reset();
        void Update(float deltaTime);
        string GetLimitText();
        bool ReachedLimit();
        string GetDescription();
        float PercentComplete();
        void Increase(int amount);
    }
}
