using UnityEngine;

namespace Assets.Scripts
{
    public enum MenuTab { MainMenu, LevelSelect, Progress, Settings, Powerups }

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

        [SerializeField]
        private GameObject PowerupsTab;

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

        public void PoweurpsTab_Clicked()
        {
            ChangeTab(Scripts.MenuTab.Powerups);
        }

        private void ChangeTab(MenuTab menuTab)
        {
            LevelSelectTab.SetActive(menuTab == Scripts.MenuTab.LevelSelect);
            MenuTab.SetActive(menuTab == Scripts.MenuTab.MainMenu);
            ProgressTab.SetActive(menuTab == Scripts.MenuTab.Progress);
            SettingsTab.SetActive(menuTab == Scripts.MenuTab.Settings);
            PowerupsTab.SetActive(menuTab == Scripts.MenuTab.Powerups);

            LastTab = menuTab;
        }
    }
}