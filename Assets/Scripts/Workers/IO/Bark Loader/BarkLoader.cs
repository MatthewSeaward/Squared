using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Data_Loaders;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO
{
    public class BarkLoader : IBarkLoader
    {
        private static Dictionary<string, Barks> Barks;
        private IDataReader DataReader = new JSONDataReader();

        public Dictionary<string, Barks> GetBarks()
        {
            if (Barks == null)
            {
                Barks = LoadData<Barks>("Barks");
            }

            return Barks;
        }

        private Dictionary<string, T> LoadData<T>(string path)
        {
            var result = new Dictionary<string, T>();

            var data = Resources.LoadAll<TextAsset>(path);
            
            foreach (var jsonData in data)
            {
                result.Add(jsonData.name, DataReader.ReadSingleData<T>(jsonData.text));
            }
            return result;
        }
    }
}
