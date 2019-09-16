using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO.Helpers;
using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Workers.Helpers
{
    public class DebugLogger      
    {
        private static DebugLogger _instance;

        public static DebugLogger Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DebugLogger();
                }
                return _instance;
            }
        }

        private DebugLogger()
        {
        }

     
        public void WriteEntry(string message)
        {
            string output = $"{DateTime.Now} - {message}";

            Debug.Log(output);
        }

        public void WriteException(Exception ex)
        {
            string output = $"{DateTime.Now} - ERROR - {ex.Message}{Environment.NewLine}{ex.StackTrace}";

            Debug.LogError(output);

            var key = FireBaseDatabase.Database.Child("Exceptions").Child(UserManager.UserID).Push().Key;

            System.Threading.Tasks.Task.Run(() => FireBaseDatabase.Database.Child("Exceptions").Child(UserManager.UserID).Child(key).SetValueAsync(output));

        }


    }
}
