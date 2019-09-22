using Assets.Scripts.Workers.Enemy.Events;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;

namespace Assets.Scripts.Workers.IO.Data_Entities
{
    public class StarProgress
    {
        public int Number { get; set; }
        public IGameLimit Limit { get; set; }
        public IRestriction Restriction { get; set; }
        public bool Completed { get; set; }
        public EnemyEvents Events { get; set; }
    }
}