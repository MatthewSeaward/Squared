using Assets.Scripts.Workers.Managers;
using System.Linq;
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

            var numberOfStars = matchingLevels.Sum(x => x.StarAchieved);

            GetComponentInChildren<ProgressBar>().UpdateProgressBar(numberOfStars, matchingLevels.Length * 3, $"{numberOfStars}/{matchingLevels.Length * 3}");
        }
    }
}
