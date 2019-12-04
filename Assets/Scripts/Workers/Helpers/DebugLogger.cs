using Assets.Scripts.Workers.IO.Helpers;
using System;
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

            FireBaseDatabase.AddUniqueString(FireBaseSavePaths.ExceptionLocation(), output);
        }
    }
}