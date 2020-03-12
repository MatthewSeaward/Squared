using System;
using System.Collections.Generic;
using Assets.Scripts.Workers.Core;
using Assets.Scripts.Workers.Enemy;
using Assets.Scripts.Workers.Enemy.Events;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.IO.Data_Entities;
using UnityEngine;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;
using static SquarePiece;

namespace Assets.Scripts.Workers.Factorys
{
    public static class EnemyEventsFactory
    {

        public static EnemyEvents GetLevelEvents(LevelEvents.EventTrigger[] levelEvent)
        {
            var events = new EnemyEvents();

            if (levelEvent == null)
            {
                return events;
            }
                       
            foreach (var e in levelEvent)
            {
                var result = GetEnemyEvent(e);
                if (result == null)
                {
                    continue;
                }

                events.RageEvents.Add(result);       
            }
            return events;
        }

        private static EnemyEventTrigger GetEnemyEvent(LevelEvents.EventTrigger e)
        {
            var enemyEvent = GetEnemyEventTrigger(e);

            enemyEvent.EnemyRage = GetEnemyRageEvents(e);

            return enemyEvent;
        }

        private static EnemyEventTrigger GetEnemyEventTrigger(LevelEvents.EventTrigger e)
        {
            switch(e.Trigger)
            {
                case "Turns":
                    return new TurnEventTrigger(new Range(e.TriggerOn));
                case "Percent":
                    return new PiecePercentEventTrigger(e.TriggerOn);
                default:
                    throw new ArgumentException($"Invalid Trigger specified. Trigger specified was {e.Trigger}");
            }
        }

        private static List<IEnemyRage> GetEnemyRageEvents(LevelEvents.EventTrigger trigger)
        {
            var list = new List<IEnemyRage>();
            if (trigger.Events == null)
            {
                Debug.LogError($"EventTrigger has null events. Trigger {trigger.Trigger} and TriggerOn {trigger.TriggerOn}");
            }
            else
            {
                foreach (var e in trigger.Events)
                {
                    var rageEvent = GetEnemyRageEvent(e);
                    if (rageEvent != null)
                    {
                        list.Add(rageEvent);
                    }
                }
            }
            return list;
        }

        private static IEnemyRage GetEnemyRageEvent(LevelEvents.Event e)
        { 
            switch (e.EventType)
            { 
                case "Destroy":
                     return new DestroyRage() { SelectionAmount = e.NumberOfPiecesToSelect };
                case "Swap":
                      return new SwapRage() { SelectionAmount = e.NumberOfPiecesToSelect };
                case "Change":
                    return new ChangeRandomPiece() { SelectionAmount = e.NumberOfPiecesToSelect, NewPieceType = e.GetNewPieceType() };
                case "ChangeS":
                    return new ChangeSpecificPiece(e.GetTypeOfPieceToSelect(), e.GetNewPieceType()) { SelectionAmount = e.NumberOfPiecesToSelect };
                case "ChangeC":
                    return new ChangeColourEvent((Colour)(Enum.Parse(typeof(Colour), e.TypeOfPieceToSelect)), (Colour)(Enum.Parse(typeof(Colour), e.NewPieceType))) { SelectionAmount = e.NumberOfPiecesToSelect };
                case "Add":
                    return new AddPieceEvent(GetPositions(e.PositionsSelected), (PieceTypes) e.NewPieceType[0]);
                case "Remove":
                    return new RemovePieceEvent(GetPositions(e.PositionsSelected));
                case "AddC":
                    return new AddSpriteEvent((Colour) (Enum.Parse(typeof(Colour), e.NewPieceType)));
                case "RemoveC":
                    return new RemoveSpriteEvent((Colour)(Enum.Parse(typeof(Colour), e.NewPieceType)));
                default:
                    throw new ArgumentException($"Invalid EventType specified. EventType specified was {e.EventType}");
            }
        }

        private static List<Vector2Int> GetPositions(string[] positionsSelected)
        {
            var positions = new List<Vector2Int>();
            foreach (var s in positionsSelected)
            {
                var parts = s.Split(':');
                positions.Add(new Vector2Int(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1])));
            }

            return positions;
        }
       

      
    }
}
