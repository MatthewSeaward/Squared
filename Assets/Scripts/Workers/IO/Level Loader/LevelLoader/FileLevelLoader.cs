using Assets.Scripts.Workers.IO.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Workers.IO.Interfaces;

namespace Assets.Scripts.Workers.IO
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