using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.Scripts.Workers.Managers
{
    public static class UserManager
    {
        private static string _username;

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
    }
}