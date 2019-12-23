using UnityEngine;

namespace Assets.Scripts.Workers.Helpers
{
    public static class DebugHelper
    {
        private static bool _forceCancelDebugMode = false;

        public static bool InDebugMode
        {
            get
            {
                if (_forceCancelDebugMode)
                {
                    return false;
                }
                else
                {
                    return Debug.isDebugBuild;
                }
            }
        }

        public static void ForceCancelDebugMode()
        {
            _forceCancelDebugMode = true;
        }
    }
}
