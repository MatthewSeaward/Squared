using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Workers.Enemy;
using Assets.Scripts.Workers.Enemy.Events;
using Assets.Scripts.Workers.Helpers.Extensions;

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

                var type = parts.SafeGet(0);
                var trigger = parts.SafeGet(1);
                var min = parts.SafeGet(2);
                var max = parts.SafeGet(3);
                var amount = parts.SafeGet(4);
                var parameters = parts.SafeGet(5);

                eEvent.RageEvents.Add(BuildTrigger(type, trigger, min, max, amount, parameters));
            }

            return eEvent;
        }

        private static EnemyEventTrigger BuildTrigger(string type, string trigger, string min, string max, string amount, string parameters)
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
                case "Change":
                    trig.EnemyRage = new ChangePiece() { SelectionAmount = Convert.ToInt32(amount), NewPieceType = (PieceFactory.PieceTypes) parameters[0]  };
                    break;
            }

            return trig;
        }
    }
}
