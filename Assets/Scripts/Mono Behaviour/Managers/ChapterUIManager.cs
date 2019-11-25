using System;
using UnityEngine;

namespace Assets.Scripts
{
    class ChapterUIManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] Backgrounds;

        [SerializeField]
        private GameObject PreviousButton;

        [SerializeField]
        private GameObject NextButton;

        private void OnEnable()
        {
            UpdateBackgrounds();
        }

        private void UpdateBackgrounds()
        {
            foreach (var background in Backgrounds)
            {
                bool active = background.name == LevelManager.Instance.SelectedChapter;
                background.SetActive(active);

                if (active)
                {
                    background.GetComponentInChildren<Animator>().SetTrigger("Load");
                }
            }
            FindObjectOfType<MenuLevelSelection>().Refresh();

            PreviousButton.SetActive(LevelManager.Instance.IsPreviousChapter());
            NextButton.SetActive(LevelManager.Instance.IsNextChapter());
        }

        public void NextChapter()
        {
            LevelManager.Instance.NextChapter();
            UpdateBackgrounds();
        }

        public void PreviousChapter()
        {
            LevelManager.Instance.PreviousChapter();
            UpdateBackgrounds();
        }
    }
}
