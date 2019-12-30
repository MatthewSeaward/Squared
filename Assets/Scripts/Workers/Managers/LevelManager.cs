using UnityEngine;
using System.Linq;
using Assets.Scripts.Workers.IO;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Workers.Level_Info;
using Assets.Scripts.Workers.Managers;

namespace Assets.Scripts.Workers.Managers
{
    public class LevelManager
    {
        private Dictionary<string, Level[]> _levels;
        private string[] _levelOrder;

        private int chapterInt = 0;
        private int _currentLevel = 0;

        public Dictionary<string, Level[]> Levels
        {
            get
            {
                if (_levels == null)
                {
                    _levels = new Dictionary<string, Level[]>();
                }
                return _levels;
            }
            private set
            {
                _levels = value;
            }
        }

        private string[] LevelOrder
        {
            get
            {
                if (_levelOrder == null)
                {
                    _levelOrder = new string[0];
                }
                return _levelOrder;
            }
            set
            {
                _levelOrder = value;
            }
        }   

        public int CurrentStars
        {
            get
            {
                int count = 0;
                foreach (var lvl in Levels.Values)
                {
                    lvl.ToList().ForEach(x => count += x.StarAchieved);
                }
                return count >= 0 ? count : 0;
            }
        }

        public Level[] SelectedChapterLevels => Levels.ContainsKey(SelectedChapter) ? Levels[SelectedChapter] : new Level[0];

        public string SelectedChapter => LevelOrder[chapterInt];         

        public int CurrentLevel
        {
            get
            {
                return _currentLevel;
            }
            set
            {
                _currentLevel = value;
                SelectedLevel = GetNextLevel();
            }
        }

        public Level SelectedLevel { get; set; }        

        private static LevelManager _instance;
        public static LevelManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LevelManager();
                }
                return _instance;
            }
        }

        private LevelManager()
        {
            ScoreKeeper.GameCompleted += ScoreKeeper_GameCompleted;
            ResetData.ResetAllData += ResetSavedData;
        }

        ~LevelManager()
        {
            ScoreKeeper.GameCompleted -= ScoreKeeper_GameCompleted;
            ResetData.ResetAllData -= ResetSavedData;
        }

        private void ScoreKeeper_GameCompleted(string chapter, int level, int star, int score, GameResult result)
        {
            if (result == GameResult.ReachedTarget)
            {                
                RegisterLevelCompleted(star);
            }
        }

        public Level GetNextLevel()
        {
            if (CurrentLevel >= SelectedChapterLevels.Length)
            {
                CurrentLevel = SelectedChapterLevels.Length - 1;
            }
            return SelectedLevel = SelectedChapterLevels[CurrentLevel] ;
        }

        public int GetStarAchievedOnLevel(int level)
        {
            if (level < SelectedChapterLevels.Length)
            {
                return SelectedChapterLevels[level].StarAchieved;
            }
            else
            {
                return 0;
            }
        }

        internal void RegisterLevelCompleted(int star)
        {
            SelectedLevel.StarAchieved = star;
            LevelIO.Instance.SaveLevelProgress(SelectedChapter, CurrentLevel, Mathf.Clamp(star, 0, 3));
        }

        public void SetLevelProgress(Scripts.Workers.IO.Data_Entities.LevelProgress[] loadedData)
        {
            var levelProgress = new List<Scripts.Workers.IO.Data_Entities.LevelProgress>();
            if (loadedData != null)
            {
                levelProgress.AddRange(loadedData);
            }

            foreach (var progress in levelProgress)
            {
                if (!Levels.ContainsKey(progress.Chapter))
                {
                    continue;
                }

                var levels = Levels[progress.Chapter];
                if (progress.Level >= levels.Length)
                {
                    continue;
                } 

                levels[progress.Level].StarAchieved = progress.StarAchieved;
            }
        }

        private bool LevelUnlocked(int i)
        {
            if (i >= SelectedChapterLevels.Length)
            {
                return false;
            }
            else
            {
                return CurrentStars >= SelectedChapterLevels[i].StarsToUnlock;
            }
        }

        public bool CanPlayLevel(int levelNumber)
        {
            return LivesManager.Instance.LivesRemaining > 0 && LevelUnlocked(levelNumber);        
        }

        public bool IsNextChapter() => chapterInt + 1 < LevelOrder.Length;
        public bool IsPreviousChapter() => chapterInt - 1 >= 0;

        public void NextChapter() =>  ChangeChapter(1);
        public void PreviousChapter() => ChangeChapter(-1);

        public void SetChapter(string chapter)
        {
            if (!LevelOrder.Contains(chapter))
            {
                return;
            }

            chapterInt = LevelOrder.ToList().IndexOf(chapter);
        }

        private void ChangeChapter(int direction)
        {
            chapterInt = Mathf.Clamp(chapterInt + direction, 0, LevelOrder.Length-1);
        }

        public void ResetSavedData()
        {
            LevelIO.Instance.ResetSavedData();
        }

        public void SetupLevels(string chapter, Level[] levels)
        {
            var dict = new Dictionary<string, Level[]>
            {
                { chapter, levels }
            };

            SetupLevels(dict);
        }

        public void SetupLevels(Dictionary<string, Level[]> Levels)
        {
            SetupLevels(Levels, Levels.Keys.ToArray());
        }

        public void SetupLevels(Dictionary<string, Level[]> levels, string[] levelOrder)
        {
            this.Levels = levels;
            this.LevelOrder = levelOrder;

            chapterInt = 0;
        }
    }
}
