using System.Collections.Generic;
using Assets.Scripts.Workers.Enemy.Events;
using DataEntities;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Enemy_Event
{
    class JSONEventReader : IEventReader
    {
        public Dictionary<string, LevelEvents[]> GetEvents()
        {      
            var Barks = LoadData("Level Events");
            return Barks;
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
