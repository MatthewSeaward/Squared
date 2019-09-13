using Assets.Scripts.Workers.IO.Data_Loaders;
using DataEntities;
using LevelLoader.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelLoader
{
    class FileLevelLoader : ILevelLoader
    {
        private static Dictionary<string, Level[]> Levels;
        private IDataReader DataReader = new JSONDataReader();

        public Dictionary<string, Level[]> GetLevels()
        {            
            Levels = LoadData<Level>("Levels");

            var dialogue = LoadData<LevelDialogue>("Level Dialogue");

            foreach (var chapter in Levels)
            {
                if (!dialogue.ContainsKey(chapter.Key))
                {
                    continue;
                }

                foreach (var lvl in chapter.Value)
                {
                    var dialogueItem = dialogue[chapter.Key].FirstOrDefault(x => x.LevelNumber == lvl.LevelNumber);
                    if (dialogueItem == null)
                    {
                        continue;
                    }

                    lvl.DiaglogueItems = dialogueItem;
                }
            }            

            return Levels;
        }

        private Dictionary<string, T[]> LoadData<T>(string path)
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
