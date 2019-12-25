using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Enemy_Event;
using Assets.Scripts.Workers.IO.Level_Loader.Order;
using LevelLoader.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Workers.IO
{
    public delegate void DataLoaded();

    public class LevelIO
    {
        private static LevelIO _instance;

        private ILevelLoader levelLoader;
        private ILevelProgressReader progressReader;
        private ILevelProgressWriter progressWriter;
        private ILevelOrderLoader levelOrderLoader;
        private IEventReader eventReader;

        private bool initialised = false;
        
        public static LevelIO Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LevelIO();
                }
                return _instance;
            }
        }

        private LevelIO()
        {
        }

        public void Initialise(ILevelLoader levelLoader, ILevelProgressReader progressReader, ILevelProgressWriter progressWriter, ILevelOrderLoader levelOrderLoader, IEventReader eventReader)
        {
            if (initialised)
            {
                Debug.Log("LevelIO has already been initalised");
                return;
            }

            this.progressReader = progressReader;
            this.progressWriter = progressWriter;
            this.levelOrderLoader = levelOrderLoader;
            this.levelLoader = levelLoader;
            this.eventReader = eventReader;

            initialised = true;
        }

        public void LoadLevels()
        {
            CheckForInitialisation();

            var Levels = levelLoader.GetLevels();
            var LevelOrder = levelOrderLoader.LoadLevelOrder();

            LevelManager.Instance.SetupLevels(Levels, LevelOrder);
        }

        public void LoadLevelStars()
        {
            CheckForInitialisation();
            
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
            CheckForInitialisation();

            var events = eventReader.GetEvents();

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

        public async Task LoadLevelProgress()
        {
            CheckForInitialisation();

            var levelProgress = await progressReader.LoadLevelProgress();
            LevelManager.Instance.SetLevelProgress(levelProgress);
        }

        internal void SaveLevelProgress(int level, LevelProgress levelinfo)
        {
            CheckForInitialisation();

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
            CheckForInitialisation();

            progressWriter.ResetData();
        }

        private void CheckForInitialisation()
        {
            if (!initialised)
            {
                throw new InvalidOperationException("LevelIO has not been initialised. Call the initialise method first");
            }
        }

        public void Reset()
        {
            _instance = null;
            initialised = false;
        }
    }
}