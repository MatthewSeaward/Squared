using Assets.Scripts.Workers.IO.Helpers;
using DataEntities;
using LevelLoader.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace LevelLoader
{
    class FileLevelLoader : ILevelLoader
    {
        public Dictionary<string, Level[]> GetLevels()
        {
            Dictionary<string, Level[]> levels = null;
            Dictionary<string, LevelDialogue[]> dialogue = null;

            levels = TextAssetHelpers.LoadData<Level>("Levels");
            dialogue = TextAssetHelpers.LoadData<LevelDialogue>("Level Dialogue");

            foreach (var chapter in levels)
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

            return levels;
        }
    }
}