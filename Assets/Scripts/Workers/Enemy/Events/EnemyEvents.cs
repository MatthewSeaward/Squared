using System;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Enemy.Events
{
    public class EnemyEvents
    {
        public List<EnemyEventTrigger> RageEvents = new List<EnemyEventTrigger>();

        internal void Update(float deltaTime)
        {
            foreach(var rageEvent in RageEvents)
            {
                rageEvent.Update(deltaTime);
            }
        }
    }
}