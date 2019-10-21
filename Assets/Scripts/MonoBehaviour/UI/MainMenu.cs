using UnityEngine;

namespace Assets.Scripts
{
    class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject LevelSelect;

        [SerializeField]
        private GameObject menu;

        public void Start()
        {
            BackToMainMenu();
        }

        public void OnChapterSelected(string chapter)
        {
            LevelManager.Instance.SetChapter(chapter);
            LevelSelect.SetActive(true);
            menu.SetActive(false);
        }

        public void BackToMainMenu()
        {
            LevelSelect.SetActive(false);
            menu.SetActive(true);
        }
    }
}
