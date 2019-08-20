using Assets.Scripts.Workers.IO.Data_Entities;
using DataEntities;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Workers.IO;
using System.Collections.Generic;

namespace Assets
{
    public class LevelManager
    {
        public Dictionary<string, Level[]> Levels;

        public string[] LevelOrder { get; }

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
                SelectedChapterLevels.ToList().ForEach(x =>  count += x.LevelProgress != null ? x.LevelProgress.StarAchieved : 0);
                return count;
            }
        }

        public Level[] SelectedChapterLevels => Levels[SelectedChapter];

        private int _currentLevel = 0;
        public string SelectedChapter => LevelOrder[chapterInt];

        internal bool LevelUnlocked(int i)
        {
            return CurrentStars >= SelectedChapterLevels[i].StarsToUnlock;
        }

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

            SelectedChapterLevels[CurrentLevel].LevelProgress.StarAchieved = Mathf.Clamp(star + 1, 0, 3);
            SelectedChapterLevels[CurrentLevel].LevelProgress.HighestScore = Mathf.Max(SelectedChapterLevels[CurrentLevel].LevelProgress.HighestScore, score);
            SelectedChapterLevels[CurrentLevel].LevelProgress.Chapter = SelectedChapter;

            LevelIO.SaveLevelProgress(CurrentLevel, SelectedChapterLevels[CurrentLevel].LevelProgress);
        }

        public void NextChapter() =>   ChangeChapter(1);
        public void PreviousChapter() => ChangeChapter(-1);

        private void ChangeChapter(int direction)
        {
            chapterInt = Mathf.Clamp(chapterInt + direction, 0, LevelOrder.Length-1);
        }
    }
}
