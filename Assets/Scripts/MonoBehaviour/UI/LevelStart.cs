using Assets.Scripts.Constants;
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
               
        public void OnEnable()
        {
            Level.text = "Level " + (LevelManager.Instance.CurrentLevel + 1).ToString();
            Target.text = LevelManager.Instance.SelectedLevel.Target.ToString();
            Limit.text = LevelManager.Instance.SelectedLevel.GetCurrentLimit().GetDescription();
            Restriction.text = LevelManager.Instance.SelectedLevel.GetCurrentRestriction().GetRestrictionDescription();

            GetComponentInChildren<StarPanel>().Configure(LevelManager.Instance.SelectedLevel.LevelProgress);
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
