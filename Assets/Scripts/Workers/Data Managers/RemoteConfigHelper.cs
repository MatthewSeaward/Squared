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
                Debug.LogError($"Error Loading Collection Interval for type {type}.{Environment.NewLine}{e.ToString()}");
            }
            return 50;
        }
    }
}
