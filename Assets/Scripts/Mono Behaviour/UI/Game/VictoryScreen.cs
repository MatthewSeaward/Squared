using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.Constants;

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
            Continue.interactable = LevelManager.Instance.LevelUnlocked(LevelManager.Instance.CurrentLevel + 1);
            NextStar.gameObject.SetActive(LevelManager.Instance.SelectedLevel.LevelProgress.StarAchieved < 3);
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
