using Assets.Scripts.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class InGameMenu : MonoBehaviour
    {
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
