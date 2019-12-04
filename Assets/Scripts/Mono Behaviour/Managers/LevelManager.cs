using Assets.Scripts.Workers.IO.Data_Entities;
using DataEntities;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Workers.IO;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.Workers.Data_Managers;

namespace Assets
{
    public class LevelManager
    {
        public Dictionary<string, Level[]> Levels;

        public string[] LevelOrder { get; set; }

        private LevelIO LevelIO = new LevelIO();
        private int chapterInt = 0;

        private int LevelCompleted
        {
            get
            {
                if (Debug.isDebugBuild)
                {
                    return SelectedChapterLevels.Length - 1;
                }

                return SelectedChapterLevels.Where(x => x.LevelProgress != null).Select(x => x.LevelProgress.Level).First();
            }
        }

        public int CurrentStars
        {
            get
            {
                int count = 0;
                foreach (var lvl in Levels.Values)
                {
                    lvl.ToList().ForEach(x => count += x.LevelProgress != null ? x.LevelProgress.StarAchieved : 0);
                }
                return count;
            }
        }

        public Level[] SelectedChapterLevels => Levels[SelectedChapter];

        private int _currentLevel = 0;
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
                RegisterLevelCompleted(star, score);
            }
        }

        public void LoadData()
        {
            LevelIO.LoadLevelData();
            Levels = LevelIO.Levels;
            LevelOrder = LevelIO.LevelOrder;
        }

        public bool HasCompletedLevel(int i)
        {
            return i <= LevelCompleted;
        }

        public Level GetNextLevel()
        {
            if (CurrentLevel > SelectedChapterLevels.Length)
            {
                CurrentLevel = SelectedChapterLevels.Length - 1;
            }
            return SelectedLevel = SelectedChapterLevels[CurrentLevel] ;
        }

        public LevelProgress GetLevelProgress(int level)
        {
            if (level < SelectedChapterLevels.Length)
            {
                return SelectedChapterLevels[level].LevelProgress;
            }
            else
            {
                return null;
            }
        }

        internal void RegisterLevelCompleted(int star, int score)
        {
            if (SelectedChapterLevels[CurrentLevel].LevelProgress == null)
            {
                SelectedChapterLevels[CurrentLevel].LevelProgress = new LevelProgress() { Level = CurrentLevel };
            }

            SelectedChapterLevels[CurrentLevel].LevelProgress.StarAchieved = Mathf.Clamp(star, 0, 3);
            SelectedChapterLevels[CurrentLevel].LevelProgress.HighestScore = Mathf.Max(SelectedChapterLevels[CurrentLevel].LevelProgress.HighestScore, score);
            SelectedChapterLevels[CurrentLevel].LevelProgress.Chapter = SelectedChapter;

            LevelIO.SaveLevelProgress(CurrentLevel, SelectedChapterLevels[CurrentLevel].LevelProgress);
        }

        internal bool LevelUnlocked(int i)
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
            return LivesManager.Instance.LivesRemaining > 0 && LevelUnlocked (levelNumber);        
        }

        public bool IsNextChapter() =>  chapterInt + 1 < LevelOrder.Length;
        public bool IsPreviousChapter() => chapterInt - 1 >= 0;

        public void NextChapter() =>   ChangeChapter(1);
        public void PreviousChapter() => ChangeChapter(-1);

        public void SetChapter(string chapter)
        {
            chapterInt = LevelOrder.ToList().IndexOf(chapter);
        }

        private void ChangeChapter(int direction)
        {
            chapterInt = Mathf.Clamp(chapterInt + direction, 0, LevelOrder.Length-1);
        }

        public void ResetSavedData()
        {
            LevelIO.ResetSavedData();
        }
    }
}
