using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.Constants;
using Assets.Scripts.Workers.Data_Managers;

namespace Assets.Scripts
{
    class VictoryScreen : MonoBehaviour
    {
        [SerializeField]
        private Button Menu;

        [SerializeField]
        private Button NextStar;

        [SerializeField]
        private Button Continue;

        [SerializeField]
        private Text Body;

        public void Show(string body)
        {
            Body.text = body;
            Continue.interactable = LevelManager.Instance.CanPlayLevel(LevelManager.Instance.CurrentLevel + 1);
            NextStar.gameObject.SetActive(LivesManager.Instance.LivesRemaining > 0 && LevelManager.Instance.SelectedLevel.LevelProgress.StarAchieved < 3);
            GetComponentInChildren<StarPanel>().Configure(LevelManager.Instance.SelectedLevel.LevelProgress);
            gameObject.SetActive(true);
        }

        public void LoadNextLevel()
        {
            LevelManager.Instance.CurrentLevel++;
            SceneManager.LoadScene(Scenes.Game);
        }

        public void ReloadLevel()
        {
            SceneManager.LoadScene(Scenes.Game);
        }

        public void BackToMenu()
        {
            SceneManager.LoadScene(Scenes.MainMenu);
        }
    }
}
