using Assets.Scripts.Constants;
using Assets.Scripts.Mono_Behaviour.UI.Components;
using Assets.Scripts.Workers.Managers;
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
               
        public void OnEnable()
        {
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
