using Assets.Scripts.Workers.IO.Data_Loaders;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Helpers
{
    public static class TextAssetHelpers
    {
        private static IDataReader DataReader = new JSONDataReader();

        public static Dictionary<string, T[]> LoadData<T>(string path)
        {            
            var data = Resources.LoadAll<TextAsset>(path);
            var list = new Dictionary<string, T[]>();

            foreach (var jsonData in data)
            {
                var decodedData = DataReader.ReadData<T>(jsonData.text);
                list.Add(jsonData.name, decodedData);
            }

            return list;           
        }
    }
}
