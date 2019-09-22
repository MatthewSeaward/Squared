using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Workers.Enemy;
using Assets.Scripts.Workers.Enemy.Events;

namespace Assets.Scripts.Workers.IO
{
    public static class EnemyEventsFactory
    {
        internal static EnemyEvents GetLevelEvents(string[] star1)
        {
            ///					"Destroy,Turns,7,8",
                var eEvent = new EnemyEvents();

            foreach (var str in star1)
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    continue;
                }

                var parts = str.Split(',');

                var type = parts[0];
                var trigger = parts[1];
                var min = parts[2];
                var max = parts[3];
                var amount = parts[4];

                eEvent.RageEvents.Add(BuildTrigger(type, trigger, min, max, amount));
            }

            return eEvent;
        }

        private static EnemyEventTrigger BuildTrigger(string type, string trigger, string min, string max, string amount)
        {
            EnemyEventTrigger trig = null;

            switch(trigger)
            {
                case "Turns":
                    trig = new TurnEventTrigger(Convert.ToInt32(min), Convert.ToInt32(max));
                    break;
            }

            switch (type)
            {
                case "Destroy":
                    trig.EnemyRage = new DestroyRage() { SelectionAmount = Convert.ToInt32(amount) };
                    break;
                case "Swap":
                    trig.EnemyRage = new SwapRage() { SelectionAmount = Convert.ToInt32(amount) };
                    break;
            }

            return trig;
        }
    }
}
