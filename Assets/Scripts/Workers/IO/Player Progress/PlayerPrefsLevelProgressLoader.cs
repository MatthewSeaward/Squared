using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Workers.IO
{
    class PlayerPrefsLevelProgressLoader : ILevelProgressReader
    {
        public async Task<LevelProgress[]> LoadLevelProgress()
        {
            return await Task.Run(() =>
            {
                var playerPrefs = PlayerPrefs.GetString("LevelProgress");
                if (string.IsNullOrEmpty(playerPrefs))
                {
                    return new LevelProgress[0];
                }

                var fromJSON = JsonHelper.FromJson<LevelProgress>(playerPrefs);
                if (fromJSON == null)
                {
                    return new LevelProgress[0];
                }

                return fromJSON;
            }
        );
        }
    }
}
