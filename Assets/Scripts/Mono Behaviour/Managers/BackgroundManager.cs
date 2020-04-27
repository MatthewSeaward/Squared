using Assets.Scripts.Workers.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    class BackgroundManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] Backgrounds;

        private void Start()
        {
            var chapterName = LevelManager.Instance.DailyChallenge ? "Default" : LevelManager.Instance.SelectedChapter;

            foreach (var background in Backgrounds)
            {

               background.SetActive(background.name == chapterName);
            }
        }
    }
}
