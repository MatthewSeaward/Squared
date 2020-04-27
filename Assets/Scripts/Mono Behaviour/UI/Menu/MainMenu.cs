using Assets.Scripts.Constants;
using Assets.Scripts.Workers.Daily_Challenge;
using Assets.Scripts.Workers.Managers;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        [SerializeField]
        private GameObject HomeButton;
        private bool _alreadyLoading;

        public void Start()
        {
            ChangeTab(LastTab);
        }

        public static void ResetLastTab()
        {
            LastTab = Scripts.MenuTab.MainMenu;
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

        public void DailyChallenge_Clicked()
        {
            LevelManager.Instance.SelectedLevel = RandomLevelGenerator.GenerateRandomLevel(new CurrentDayGenerator());
            LevelManager.Instance.DailyChallenge = true;

            StartCoroutine(LoadSceneAsync());
        }
              
        private void ChangeTab(MenuTab menuTab)
        {
            LevelSelectTab.SetActive(menuTab == Scripts.MenuTab.LevelSelect);
            MenuTab.SetActive(menuTab == Scripts.MenuTab.MainMenu);
            ProgressTab.SetActive(menuTab == Scripts.MenuTab.Progress);
            SettingsTab.SetActive(menuTab == Scripts.MenuTab.Settings);
            PowerupsTab.SetActive(menuTab == Scripts.MenuTab.Powerups);

            HomeButton.SetActive(menuTab != Scripts.MenuTab.MainMenu);

            LastTab = menuTab;
        }

        private IEnumerator LoadSceneAsync()
        {
            _alreadyLoading = true;

            //FindObjectOfType<EventSystem>().gameObject.SetActive(false);

          //  button.GetComponentInChildren<Text>().text = "PLEASE WAIT";

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scenes.Game);

            asyncLoad.allowSceneActivation = false;

            while (asyncLoad.progress < 0.9f)
            {
                yield return null;
            }

            asyncLoad.allowSceneActivation = true;
        }

    }
}