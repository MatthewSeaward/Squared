using UnityEngine;

namespace Assets.Scripts
{
    public enum MenuTab { MainMenu, LevelSelect, Progress, Settings }

    class MainMenu : MonoBehaviour
    {
        private static MenuTab LastTab = Scripts.MenuTab.MainMenu;

        [SerializeField]
        private GameObject LevelSelectTab;

        [SerializeField]
        private GameObject MenuTab;

        [SerializeField]
        private GameObject ProgressTab;

        [SerializeField]
        private GameObject SettingsTab;

        public void Start()
        {
            ChangeTab(LastTab);
        }

        public void OnChapterSelected(string chapter)
        {
            LevelManager.Instance.SetChapter(chapter);
            ChangeTab(Scripts.MenuTab.LevelSelect);
        }

        public void BackToMainMenu()
        {
            ChangeTab(Scripts.MenuTab.MainMenu);
        }

        public void ProgressTab_Clicked()
        {
            ChangeTab(Scripts.MenuTab.Progress);
        }

        public void SettingsTab_Clicked()
        {
            ChangeTab(Scripts.MenuTab.Settings);
        }

        private void ChangeTab(MenuTab menuTab)
        {
            LevelSelectTab.SetActive(menuTab == Scripts.MenuTab.LevelSelect);
            MenuTab.SetActive(menuTab == Scripts.MenuTab.MainMenu);
            ProgressTab.SetActive(menuTab == Scripts.MenuTab.Progress);
            SettingsTab.SetActive(menuTab == Scripts.MenuTab.Settings);

            LastTab = menuTab;
        }
    }
}