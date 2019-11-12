﻿using Assets.Scripts.Workers.Data_Managers;

namespace Assets.Scripts.Workers.IO.Helpers
{
    public class FireBaseSavePaths
    {
        public static string ScoreLocation(string chapter, int level, int star) => $"Scores/{chapter}/LVL {level}/Star {star}";
        public static string ScoreLocation(string chapter, int level) => $"Scores/{chapter}/LVL {level}";
        public static string PlayerProgressLocation() => $"PlayerProgress/LevelProgress/{UserManager.UserID}";
        public static string PlayerPowerupLocation() => $"PlayerProgress/Powerups/{UserManager.UserID}";
        public static string PlayerEquippedPowerupLocation() => $"PlayerProgress/Equipped Powerups/{UserManager.UserID}";
        public static string PlayerCollectionLocation() => $"PlayerProgress/PieceCollection/{UserManager.UserID}";
        public static string HeatMapLocation(string chapter, int level) => $"HeatMap/{chapter}/{level}";
        public static string ExceptionLocation() => $"Exceptions/{UserManager.UserID}";
    }
}
