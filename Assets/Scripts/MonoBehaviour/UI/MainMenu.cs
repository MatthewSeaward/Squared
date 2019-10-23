using UnityEngine;

namespace Assets.Scripts
{
    public enum MenuTab { MainMenu, LevelSelect }

    class MainMenu : MonoBehaviour
    {
        private static MenuTab LastTab = MenuTab.MainMenu;

        [SerializeField]
        private GameObject LevelSelect;

        [SerializeField]
        private GameObject Menu;

        public void Start()
        {
            ChangeTab(LastTab);
        }

        public void OnChapterSelected(string chapter)
        {
            LevelManager.Instance.SetChapter(chapter);
            ChangeTab(MenuTab.LevelSelect);
        }

        public void BackToMainMenu()
        {
            ChangeTab(MenuTab.MainMenu);
        }

        private void ChangeTab(MenuTab menuTab)
        {
            LevelSelect.SetActive(menuTab == MenuTab.LevelSelect);
            Menu.SetActive(menuTab == MenuTab.MainMenu);

            LastTab = menuTab;
        }
    }
}