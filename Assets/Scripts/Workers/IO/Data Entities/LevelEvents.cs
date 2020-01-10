using Assets.Scripts.Workers.Helpers;
using System;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;

namespace Assets.Scripts.Workers.IO.Data_Entities
{
    [Serializable]
    public class LevelEvents
    {
        public int LevelNumber;
        public EventTrigger[] Star1;
        public EventTrigger[] Star2;
        public EventTrigger[] Star3;

        [Serializable]
        public class EventTrigger
        {
            public string Trigger;
            public string TriggerOn;
            public Event[] Events;
        }

        [Serializable]
        public class Event
        {
            public string EventType;  
            public string NewPieceType;
            public string[] PositionsSelected;
            public int NumberOfPiecesToSelect;
            public string TypeOfPieceToSelect;

            public PieceTypes GetNewPieceType()
            {
                return GetPieceType(NewPieceType);
            }

            public PieceTypes GetTypeOfPieceToSelect()
            {
                return GetPieceType(TypeOfPieceToSelect);
            }

            private PieceTypes GetPieceType(string pieceType)
            {
                if (string.IsNullOrWhiteSpace(pieceType))
                {
                    return PieceTypes.Normal;
                }

                if(!EnumHelpers.IsValue<PieceTypes>(pieceType[0]))
                {
                    throw new ArgumentException($"Invalid piece type specificed. {pieceType} is not recognized");
                }

                return (PieceTypes)pieceType[0];
            }     
        }
    }
}