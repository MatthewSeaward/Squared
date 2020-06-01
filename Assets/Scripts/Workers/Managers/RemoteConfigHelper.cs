using Assets.Scripts.Constants;
using System;
using UnityEngine;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;
using static SquarePiece;

namespace Assets.Scripts.Workers.Managers
{
    public static class RemoteConfigHelper
    {
        public static int GetCollectionInterval(Colour type)
        {
            try
            {
                var remoteValue = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("CollectionInterval" + (int)type);

                return (int)remoteValue.LongValue;
            }
            catch(Exception e)
            {
                Debug.LogWarning($"Error Loading Collection Interval for type {type}.{Environment.NewLine}{e.ToString()}");
            }
            return 50;
        }

        public static int GetLivesRefreshTime()
        {
            try
            {
                return (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("LivesRefreshTime").LongValue;

            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error Loading LivesRefreshTime.{Environment.NewLine}{e.ToString()}");
            }
            return 10;
        }

        public static int GetMaxLives()
        {
            try
            {
                return (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("MaxLives").LongValue;

            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error Loading MaxLives.{Environment.NewLine}{e.ToString()}");
            }
            return 6;
        }

        public static int MaxCountOfPiece(PieceTypes type)
        {
            try
            {
                return (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("Max" + (char)type).LongValue;

            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error Loading Max Count for PieceType {type}.{Environment.NewLine}{e.ToString()}");
            }
            return int.MaxValue;
        }

        public static int MaxChanceToUseSpecialPiece()
        {
            try
            {
                return (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("ChanceToUseSpecialPiece").LongValue;

            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error Loading Chance To Use Special Piece.{Environment.NewLine}{e.ToString()}");
            }
            return GameSettings.ChanceToUseSpecialPiece;
        }
    }
}
