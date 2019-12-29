using System.Collections.Generic;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Enemy_Event;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Scripts.Workers.IO.Data_Entities
{
    class JSONEventReader : IEventReader
    {
        public Dictionary<string, LevelEvents[]> GetEvents()
        {
            return LoadData("Level Events");
        }

        private Dictionary<string, LevelEvents[]> LoadData(string path)
        {
            var result = new Dictionary<string, LevelEvents[]>();

            var data = Resources.LoadAll<TextAsset>(path);

            foreach (var jsonData in data)
            {
                result.Add(jsonData.name, JsonHelper.FromJson<LevelEvents>(jsonData.text));
            }
            return result;
        }
    }    
}