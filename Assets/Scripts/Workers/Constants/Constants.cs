using System;
using System.Collections.Generic;

namespace Assets.Scripts.Constants
{
    public static class Scenes
    {
        public const string Loading = "LoadingScene";
        public const string MainMenu = "MenuScene";
        public const string Game = "GameScene";
    }

    public static class BonusPoints
    {
        public static List<ValueTuple<int, int>> Points;

        public static void Setup()
        {
            Points = new List<(int, int)>()
            {
               (5, 3),
               (7, 5),
               (10, 8),
               (12, 10),
               (15, 12),
               (20, 15),
               (25, 20),
               (30, 23),
               (35, 25),
               (40, 30),
               (50, 35)
            };
        }
    }

    public static class GameSettings
    {
        public const int ChanceToUseBannedPiece = 60;
        public const int ChanceToUseSpecialPiece = 10;
        public const int AmountForAwesomeText = 7;
        public const float EnemyRageInterval = 10;
        public const float StarShooterFreq = 1f;
        public const float LoadingTextUpdateTime = 0.25f;
    }
}
