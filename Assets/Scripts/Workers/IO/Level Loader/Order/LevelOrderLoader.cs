using Assets.Scripts.Workers.IO.Data_Loaders;
using UnityEngine;
using IDataReader = Assets.Scripts.Workers.IO.Data_Loaders.IDataReader;

namespace Assets.Scripts.Workers.IO.Level_Loader.Order
{
    public class LevelOrderLoader : ILevelOrderLoader
    {
        private IDataReader DataReader = new JSONDataReader();

        public string[] LoadLevelOrder()
        {
            var data = Resources.LoadAll<TextAsset>("Level Order");

            foreach (var jsonData in data)
            {
                return DataReader.ReadData<string>(jsonData.text);
            }
            return new string[0];
        }
    }
}
