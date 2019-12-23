using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Enemy_Event;
using Assets.Scripts.Workers.IO.Level_Loader.Order;
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
        private ILevelLoader levelLoader = new FileLevelLoader();

        ILevelProgressReader progressLoader = new FireBaseLevelProgressReader();
        ILevelProgressWriter progressWriter = new FireBaseLevelProgressWriter();
        ILevelOrderLoader levelOrderLoader = new LevelOrderLoader();

        public Dictionary<string, Level[]> Levels;

        public string[] LevelOrder;

        public void LoadLevels()
        {
            var Levels = levelLoader.GetLevels();
            var LevelOrder = levelOrderLoader.LoadLevelOrder();

            LevelManager.Instance.SetupLevels(Levels, LevelOrder);
        }

        public void LoadLevelStars()
        {            
            foreach (var chapter in LevelManager.Instance.Levels)
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

        public void LoadEnemyEvents()
        {
            var reader = new JSONEventReader();
            var events = reader.GetEvents();

            foreach (var chapter in LevelManager.Instance.Levels)
            {
                if (!events.ContainsKey(chapter.Key))
                {
                    continue;
                }

                var eventsForChapter = events[chapter.Key];

                foreach (var level in chapter.Value)
                {
                    var eventsForLevel = eventsForChapter.FirstOrDefault(x => x.LevelNumber == level.LevelNumber);
                    if (eventsForLevel == null)
                    {
                        continue;
                    }

                    level.Star1Progress.Events = EnemyEventsFactory.GetLevelEvents(eventsForLevel.Star1);
                    level.Star2Progress.Events = EnemyEventsFactory.GetLevelEvents(eventsForLevel.Star2);
                    level.Star3Progress.Events = EnemyEventsFactory.GetLevelEvents(eventsForLevel.Star3);
                }
            }
        }

        public void LoadLevelProgress()
        {
            progressLoader.LoadLevelProgressAsync();
        }

        internal void SaveLevelProgress(int level, LevelProgress levelinfo)
        {
            var selected = LevelManager.Instance.LevelProgress.FirstOrDefault(x => x.Level == level && x.Chapter == levelinfo.Chapter);
            if (selected == null)
            {
                LevelManager.Instance.LevelProgress.Add(levelinfo);
            }
            else
            {
                selected = levelinfo;
            }
            progressWriter.SaveLevelProgress(LevelManager.Instance.LevelProgress.ToArray());
        }

        public void ResetSavedData()
        {
            progressWriter.ResetData();
        }
    }
}
