using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts.Workers.Helpers.Extensions
{
    public class DebugLogger      
    {
        private StreamWriter stream;

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
            stream = new StreamWriter("Log.txt", true);
        }

        ~DebugLogger()
        {
            stream.Close();
        }

        public void WriteEntry(string message)
        {
            string output = $"{DateTime.Now} - {message}";

            stream.WriteLine(output);
            Debug.Log(output);
        }

        public void WriteException(Exception ex)
        {
            string output = $"{DateTime.Now} - ERROR - {ex.Message}{Environment.NewLine}{ex.StackTrace}";

            stream.WriteLine(output);
            Debug.LogError(output);
        }
    }
}
