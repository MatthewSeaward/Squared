using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.Constants;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Mono_Behaviour.UI.Game;

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
        private Text NormalBody;

        [SerializeField]
        private Text ChallengeBody;

        [SerializeField]
        private GameObject NormalView;

        [SerializeField]
        private ScoreSummaryPanel ScoreSummaryPanel;

        private void Awake()
        {
            LivesManager.LivesChanged += LivesManager_LivesChanged;
        }

        private void OnDestroy()
        {
            LivesManager.LivesChanged -= LivesManager_LivesChanged;
        }

        private void LivesManager_LivesChanged(bool gained, int newLives)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => SetupButtons());
        }

        public void Show(int score, int target)
        {

            SetupButtons();

            if (LevelManager.Instance.DailyChallenge)
            {
                NormalView.SetActive(false);
                ScoreSummaryPanel.gameObject.SetActive(true);

                ChallengeBody.text = "You scored " + score;
            }
            else
            {
                NormalView.SetActive(true);
                ScoreSummaryPanel.gameObject.SetActive(false);
                ChallengeBody.text = string.Empty;

                NormalBody.text = "You scored " + score + " out of " + target;
            }

            gameObject.SetActive(true);
        }
               
        private void SetupButtons()
        {
            if (LevelManager.Instance.DailyChallenge)
            {
                Continue.interactable = LivesManager.Instance.LivesRemaining > 0;
                Continue.GetComponentInChildren<Text>().text = "Try again";
                NextStar.gameObject.SetActive(false);
            }
            else
            {
                Continue.interactable = LevelManager.Instance.CanPlayLevel(LevelManager.Instance.CurrentLevel + 1) && LivesManager.Instance.LivesRemaining > 0; ;
                NextStar.gameObject.SetActive(LivesManager.Instance.LivesRemaining > 0 && LevelManager.Instance.SelectedLevel.StarAchieved < 3);
                NextStar.interactable = LivesManager.Instance.LivesRemaining > 0;
            }
        }

        public void LoadNextLevel()
        {
            if (!LevelManager.Instance.DailyChallenge)
            {
                LevelManager.Instance.CurrentLevel++;
            }

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
