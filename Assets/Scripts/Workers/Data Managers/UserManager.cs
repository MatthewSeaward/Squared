using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.Scripts.Workers.Data_Managers
{
    public static class UserManager
    {
        private static string _username;
        private static int _livesRemaining;

        public static string UserID
        {
            get
            {
                if (_username == null)
                {                   
                    _username = SystemInfo.deviceName + SystemInfo.deviceModel;
                    if (_username.Length > 25) _username = _username.Substring(0, 25);
                    _username = Regex.Replace(_username, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
                }
                return _username;
            }
        }

        public static int LivesRemaining
        {
            get
            {
                if (Debug.isDebugBuild && _livesRemaining == 0)
                {
                    _livesRemaining = 1;
                }
                return Mathf.Clamp(_livesRemaining, 0, 6);
            }            
        }
    }
}