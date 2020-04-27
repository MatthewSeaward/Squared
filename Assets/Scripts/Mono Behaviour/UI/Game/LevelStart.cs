using Assets.Scripts.Constants;
using Assets.Scripts.Mono_Behaviour.UI.Components;
using Assets.Scripts.Workers.Managers;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public delegate void GameStarted();

    public class LevelStart : MonoBehaviour
    {
        public static event GameStarted GameStarted;

        [SerializeField]
        private Text Level;

        [SerializeField]
        private Text Target;

        [SerializeField]
        private Text Limit;

        [SerializeField]
        private Text Restriction;

        [SerializeField]
        private Image RestrictionImage;

        [SerializeField]
        private Button Play;      

        [SerializeField]
        private GameObject NormalDisplay;

        [SerializeField]
        private GameObject DailyChallengeDisplay;
               
        public void OnEnable()
        {
            if (LevelManager.Instance.DailyChallenge)
            {
                SetupDailyChallengeDisplay();
            }
            else
            {
                SetupNormalDisplay();
            }

            Play.interactable = LivesManager.Instance.LivesRemaining > 0 || Debug.isDebugBuild;
        }

        private void SetupDailyChallengeDisplay()
        {
            DailyChallengeDisplay.SetActive(true);
            NormalDisplay.SetActive(false);

            Level.text = "Challenge";

            DailyChallengeDisplay.GetComponentInChildren<Text>().text = "Score as many points as you can within the limit: " + Environment.NewLine + Environment.NewLine +
                                                                         LevelManager.Instance.SelectedLevel.GetCurrentLimit().GetDescription();                                                                        

        }
        
        private void SetupNormalDisplay()
        {
            DailyChallengeDisplay.SetActive(false);
            NormalDisplay.SetActive(true);

            Level.text = "Level " + (LevelManager.Instance.CurrentLevel + 1).ToString();
            Target.text = LevelManager.Instance.SelectedLevel.Target.ToString();
            Limit.text = LevelManager.Instance.SelectedLevel.GetCurrentLimit().GetDescription();

            GetComponentInChildren<RestrictionInfo>().DisplayRestriction(LevelManager.Instance.SelectedLevel.GetCurrentRestriction());
            GetComponentInChildren<StarPanel>().Configure(LevelManager.Instance.SelectedLevel.StarAchieved);
        }

        public void Play_Clicked()
        {
            MenuProvider.Instance.HideMenu<LevelStart>();
            Firebase.Analytics.FirebaseAnalytics.LogEvent("GameStarted", "Level", LevelManager.Instance.CurrentLevel.ToString());
            GameStarted?.Invoke();
        }

        public void Menu_Clicked()
        {
            SceneManager.LoadScene(Scenes.MainMenu);
        }
    }
}
