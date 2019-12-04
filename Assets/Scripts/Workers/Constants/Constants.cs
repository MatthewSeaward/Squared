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
               (4, 4),
               (5, 6),
               (6, 8),
               (7, 10),
               (8, 12),
               (9, 15),
               (10, 17),
               (11, 20),
               (12, 22),
               (13, 25),
               (14, 30)
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
        public const float AdditionalSquareSensitvity = 1.25f;
        public const float InGamePieceCollectedShowTime = 2.5f;
        public const float PieceIncrementSpeed = 0.05f;
        public const float SwapPieceChangeFrequency = 5f;
        public static int LivesTimerInterval = 60000;
        public static float BestMoveSearchTimeOut = 2f;
    }
}
