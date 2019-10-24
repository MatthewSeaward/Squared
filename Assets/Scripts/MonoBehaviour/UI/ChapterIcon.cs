using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    class ChapterIcon : MonoBehaviour
    {
        public void OnEnable()
        {
            GetComponent<Button>().interactable = CheckIfUnlocked();
            UpdateLevelProgress();            
        }

        private bool CheckIfUnlocked()
        {
            var matchingLevels = LevelManager.Instance.Levels[this.name];
            var currentStars = LevelManager.Instance.CurrentStars;

            var unlocked = false;
            foreach (var level in matchingLevels)
            {
                if (currentStars >= level.StarsToUnlock)
                {
                    unlocked = true;
                    break;
                }
            }

            return unlocked;
        }

        private void UpdateLevelProgress()
        {
            var matchingLevels = LevelManager.Instance.Levels[this.name];
            var numberOfStars = 0;
            foreach (var level in matchingLevels)
            {
                if (level.LevelProgress == null)
                {
                    continue;
                }
                numberOfStars += level.LevelProgress.StarAchieved;
            }

            GetComponentInChildren<ProgressBar>().UpdateProgressBar(numberOfStars, matchingLevels.Length * 3, false);
        }
    }
}
