using Assets.Scripts.UI.Helpers;
using Assets.Scripts.Workers.Managers;
using System;
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

        private void ScoreKeeper_GameCompleted(string chapter, int level, int star, int score, GameResult result, bool dailyChallenge)
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
            MenuProvider.Instance.ShowVictoryPopup(score, target);
        }

        private void ShowLimitExpiredPopup()
        {
            MenuProvider.Instance.ShowEndOfGameScreen("You did not reach the target score within the limit.");
        }

        private void ShowRestrictionViolatedPopup()
        {
            MenuProvider.Instance.ShowEndOfGameScreen("You violated the restriction.");
        }
    
    }
}
