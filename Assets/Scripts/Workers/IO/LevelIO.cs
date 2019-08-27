using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Level_Loader.Order;
using Assets.Scripts.Workers.IO.Player_Progress;
using DataEntities;
using LevelLoader;
using LevelLoader.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Workers.IO
{
    public delegate void DataLoaded();

    public class LevelIO
    {
        public static DataLoaded DataLoaded;

        private ILevelLoader levelLoader = new FileLevelLoader();

        private ILevelProgressLoader progressLoader = new FireBaseLevelProgressLoader();

        public Dictionary<string, Level[]> Levels;

        public List<LevelProgress> LevelProgress;
        public string[] LevelOrder;

        public void LoadLevelData()
        {
            Levels = levelLoader.GetLevels();
            LoadLevelStars();

            FireBaseLevelProgressLoader.UserDataLoaded += UserDataLoaded;

            progressLoader.LoadLevelProgress();

            ILevelOrderLoader loader = new LevelOrderLoader();
            LevelOrder = loader.LoadLevelOrder();
        }

        private void UserDataLoaded(LevelProgress[] levelProgresses)
        {
            FireBaseLevelProgressLoader.UserDataLoaded -= UserDataLoaded;

            LevelProgress = new List<LevelProgress>();
            LevelProgress.AddRange(levelProgresses);

            foreach (var progress in levelProgresses)
            {
                if (!Levels.ContainsKey(progress.Chapter))
                {
                    continue;
                }
                Levels[progress.Chapter][progress.Level].LevelProgress = progress;
            }
            DataLoaded?.Invoke();
        }

        private void LoadLevelStars()
        {
            foreach (var chapter in Levels)
            {
                foreach (var level in chapter.Value)
                {
                    if (level.Star1 == null)
                    {
                        continue;
                    }
                    
                    level.Star1Progress.Number = 1;
                    level.Star2Progress.Number = 2;
                    level.Star3Progress.Number = 3;

                    level.Star1Progress.Limit = LevelStarFactory.GetStarGameLimit(level.Star1);
                    level.Star2Progress.Limit = LevelStarFactory.GetStarGameLimit(level.Star2);
                    level.Star3Progress.Limit = LevelStarFactory.GetStarGameLimit(level.Star3);

                    level.Star1Progress.Restriction = LevelStarFactory.GetStarRestriction(level.Star1);
                    level.Star2Progress.Restriction = LevelStarFactory.GetStarRestriction(level.Star2);
                    level.Star3Progress.Restriction = LevelStarFactory.GetStarRestriction(level.Star3);

                }
            }
        }

        internal void SaveLevelProgress(int level, LevelProgress levelinfo)
        {
            var selected = LevelProgress.FirstOrDefault(x => x.Level == level);
            if (selected == null)
            {
                LevelProgress.Add(levelinfo);
            }
            else
            {
                selected = levelinfo;
            }
            progressLoader.SaveLevelProgress(LevelProgress.ToArray());
        }
    }
}
