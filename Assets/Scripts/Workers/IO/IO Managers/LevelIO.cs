using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Enemy_Event;
using Assets.Scripts.Workers.IO.Interfaces;
using Assets.Scripts.Workers.Managers;
using System;
using System.Collections.Generic;
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

        private List<LevelProgress> LevelProgress = new List<LevelProgress>();

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

            var levelDTOs = levelLoader.GetLevels();
            var levelOrderDTo = levelOrderLoader.LoadLevelOrder();

            var levels = levelDTOs.ToDictionary(x => x.Key, 
                                                x => x.Value.Select(level => new Level_Info.Level(level)).ToArray());

            LoadLevelStars(levelDTOs, levels);

            LevelManager.Instance.SetupLevels(levels, levelOrderDTo);

        }

        private void LoadLevelStars(Dictionary<string, Level[]> levelDTO, Dictionary<string, Level_Info.Level[]> levels)
        {
            CheckForInitialisation();

            var events = eventReader.GetEvents();
            
            foreach (var chapter in levels)
            {
               var eventsForChapter = events.ContainsKey(chapter.Key) ? events[chapter.Key] : null;

                foreach (var level in chapter.Value)
                {
                    if (level == null)
                    {
                        continue;
                    }

                    var eventsForLevel = eventsForChapter?.FirstOrDefault(x => x.LevelNumber == level.LevelNumber);

                    var dto = levelDTO[chapter.Key].FirstOrDefault(x => x.LevelNumber == level.LevelNumber);

                    level.Star1Progress = LevelStarFactory.GetLevelStar(1, dto.Star1, eventsForLevel?.Star1);
                    level.Star2Progress = LevelStarFactory.GetLevelStar(2, dto.Star2, eventsForLevel?.Star2);
                    level.Star3Progress = LevelStarFactory.GetLevelStar(3, dto.Star3, eventsForLevel?.Star3);
                }
            }                   
        }

        public async Task LoadLevelProgress()
        {
            CheckForInitialisation();

            var progress = await progressReader.LoadLevelProgress();

            LevelManager.Instance.SetLevelProgress(progress);

            LevelProgress.Clear();
            if (progress != null)
            {
                LevelProgress.AddRange(progress);
            }
        }

        internal void SaveLevelProgress(string chapter, int level, int starAchieved)
        {
            CheckForInitialisation();

            var selected = LevelProgress.FirstOrDefault(x => x.Level == level && x.Chapter == chapter);
            if (selected == null)
            {
                LevelProgress.Add(new LevelProgress() { Chapter = chapter, Level = level, StarAchieved = starAchieved });
            }
            else
            {
                selected.StarAchieved = starAchieved;
            }

            progressWriter.SaveLevelProgress(LevelProgress.ToArray());
        }

        public void ResetSavedData()
        {
            CheckForInitialisation();

            LevelProgress.Clear();
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