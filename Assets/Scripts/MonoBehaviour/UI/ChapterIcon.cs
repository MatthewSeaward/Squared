using System.Linq;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class ChapterIcon : MonoBehaviour
    {
        public void OnEnable()
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

            var calculatedProgress =(float) numberOfStars / (float) (matchingLevels.Length *3);
            var percent =(int)(calculatedProgress * 100);

            GetComponentInChildren<ProgressBar>().UpdateProgressBar(percent);

        }
    }
}
