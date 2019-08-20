using System;
using UnityEngine;

namespace Assets.Scripts
{
    class ChapterUIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] Backgrounds;

        private void Start()
        {
            updateBackgrounds();
        }

        private void updateBackgrounds()
        {
            foreach (var background in Backgrounds)
            {
                background.SetActive(background.name == LevelManager.Instance.SelectedChapter);
            }
            FindObjectOfType<MenuLevelSelection>().Refresh();
        }

        public void NextChapter()
        {
            LevelManager.Instance.NextChapter();
            updateBackgrounds();
        }

        public void PreviousChapter()
        {
            LevelManager.Instance.PreviousChapter();
            updateBackgrounds();
        }
    }
}
