using System;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.Workers.Data_Managers
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
                //return (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("LivesRefreshTime").LongValue;

            }
            catch (Exception e)
            {
                Debug.LogWarning($"Error Loading LivesRefreshTime.{Environment.NewLine}{e.ToString()}");
            }
            return 1;
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
    }
}
