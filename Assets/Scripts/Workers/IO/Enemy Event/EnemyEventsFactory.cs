using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Workers.Enemy;
using Assets.Scripts.Workers.Enemy.Events;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.Helpers.Extensions;
using UnityEngine;
using static PieceFactory;

namespace Assets.Scripts.Workers.IO
{
    public static class EnemyEventsFactory
    {
        internal static EnemyEvents GetLevelEvents(string[] star1)
        {
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
                var parameters = parts.SafeGet(5)?.Split(':');

                eEvent.RageEvents.Add(BuildTrigger(type, trigger, min, max, amount, parameters));
            }

            return eEvent;
        }

        private static EnemyEventTrigger BuildTrigger(string type, string trigger, string min, string max, string amount, string[] parameters)
        {
            EnemyEventTrigger trig = null;

            try
            {

                switch (trigger)
                {
                    case "Turns":
                        trig = new TurnEventTrigger(Convert.ToInt32(min), Convert.ToInt32(max));
                        break;
                    case "PiecePercentage":
                        trig = new PiecePercentEventTrigger((PieceTypes)min[0], Convert.ToInt32(max));
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
                        trig.EnemyRage = new ChangeRandomPiece() { SelectionAmount = Convert.ToInt32(amount), NewPieceType = (PieceFactory.PieceTypes)parameters[0][0] };
                        break;
                    case "ChangeS":
                        var from = parameters[0][0];
                        var to = parameters[1][0];

                        trig.EnemyRage = new ChangeSpecificPiece((PieceFactory.PieceTypes)from, (PieceFactory.PieceTypes)to) { SelectionAmount = Convert.ToInt32(amount) };
                        break;
                    case "Add":

                        var positions = new List<Vector2Int>();
                        foreach (var s in amount.Split(';'))
                        {
                            var parts = s.Split(':');
                            positions.Add(new Vector2Int(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1])));
                        }

                        trig.EnemyRage = new AddPieceEvent(positions, (PieceFactory.PieceTypes)parameters[0][0]);
                        break;
                    case "Remove":

                        var positions2 = new List<Vector2Int>();
                        foreach (var s in amount.Split(';'))
                        {
                            var parts = s.Split(':');
                            positions2.Add(new Vector2Int(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1])));
                        }

                        trig.EnemyRage = new RemovePieceEvent(positions2);
                        break;
                }
            }
            catch
            {
                var param = parameters != null ? string.Join(",", parameters) : string.Empty;
                Debug.LogError($"Error Processing: {type}, {trigger}, {min}, {max}, {amount}, {param}");
            }
            return trig;
        }
    }
}
