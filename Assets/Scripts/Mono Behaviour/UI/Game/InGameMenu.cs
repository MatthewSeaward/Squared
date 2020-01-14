using Assets.Scripts.Constants;
using Assets.Scripts.Workers.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField]
        private Button Restart;

        public void OnEnable()
        {
            Restart.interactable = LivesManager.Instance.LivesRemaining > 0;
        }

        public void Continue_Clicked()
        {
            MenuProvider.Instance.HideMenu<InGameMenu>();
        }

        public void Restart_Clicked()
        {
            SceneManager.LoadScene(Scenes.Game);
        }

        public void Menu_Clicked()
        {
            SceneManager.LoadScene(Scenes.MainMenu);
        }
    }
}