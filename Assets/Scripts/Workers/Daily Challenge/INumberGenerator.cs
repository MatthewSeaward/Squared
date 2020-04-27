namespace Assets.Scripts.Workers.Daily_Challenge
{
    public interface INumberGenerator
    {
        int Range(int min, int max);
        int Next();
    }
}
