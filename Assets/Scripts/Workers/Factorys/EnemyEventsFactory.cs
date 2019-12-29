using System;
using System.Collections.Generic;
using Assets.Scripts.Workers.Core;
using Assets.Scripts.Workers.Enemy;
using Assets.Scripts.Workers.Enemy.Events;
using Assets.Scripts.Workers.Enemy.Piece_Selection_Validator;
using Assets.Scripts.Workers.IO.Data_Entities;
using UnityEngine;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;

namespace Assets.Scripts.Workers.Factorys
{
    public static class EnemyEventsFactory
    {

        public static EnemyEvents GetLevelEvents(LevelEvents.Event[] levelEvent)
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

        private static EnemyEventTrigger GetEnemyEvent(LevelEvents.Event e)
        {
            var enemyEvent = GetEnemyEventTrigger(e);

            enemyEvent.EnemyRage = GetEnemyRage(e);

            return enemyEvent;
        }

        private static EnemyEventTrigger GetEnemyEventTrigger(LevelEvents.Event e)
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

        private static IEnemyRage GetEnemyRage(LevelEvents.Event e)
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
                case "Add":
                    return new AddPieceEvent(GetPositions(e.PositionsSelected), (PieceTypes) e.NewPieceType[0]);
                case "Remove":
                    return new RemovePieceEvent(GetPositions(e.PositionsSelected));
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
