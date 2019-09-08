using Assets.Scripts.UI.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Assets.Scripts.Constants.Scenes;

namespace Assets.Scripts
{
    class EndOfGameMenuProvider : MonoBehaviour
    {
        public void Awake()
        {
            ScoreKeeper.GameCompleted += ScoreKeeper_GameCompleted;
        }

        public void OnDestroy()
        {
            ScoreKeeper.GameCompleted -= ScoreKeeper_GameCompleted;
        }

        private void ScoreKeeper_GameCompleted(string chapter, int level, int star, int score, GameResult result)
        {
            switch(result)
            {
                case GameResult.ReachedTarget:
                    ShowVictoryPopup(score, LevelManager.Instance.SelectedLevel.Target);
                    break;
                case GameResult.LimitExpired:
                    ShowLimitExpiredPopup();
                    break;
                case GameResult.ViolatedRestriction:
                    ShowRestrictionViolatedPopup();
                    break;
            }
        }

        private void ShowVictoryPopup(int score, int target)
        {
            MenuProvider.Instance.ShowPopup
            (
                    "Victory", $"You scored {score} out of {target}!",
                    new ButtonArgs()
                    {
                        Text = "Continue",
                        Enabled = LevelManager.Instance.LevelUnlocked(LevelManager.Instance.CurrentLevel + 1),
                        Action = () => LoadNextLevel()
                    },
                    new ButtonArgs()
                    {
                        Text = "Retry",
                        Action = () => SceneManager.LoadScene(Game)
                    }
            );
        }

        private void ShowLimitExpiredPopup()
        {
            MenuProvider.Instance.ShowPopup("Failed", "You did not reach the target score within the limit.", new ButtonArgs()
            {
                Text = "Retry",
                Action = () => SceneManager.LoadScene(Game)
            });
        }

        private void ShowRestrictionViolatedPopup()
        {
            MenuProvider.Instance.ShowPopup("Failed", "You violated the restriction.",
                new ButtonArgs()
                {
                    Text = "Retry",
                    Action = () => SceneManager.LoadScene(Game)
                });
        }

        public void LoadNextLevel()
        {
            LevelManager.Instance.CurrentLevel++;
            SceneManager.LoadScene(Game);
        }
    }
}
