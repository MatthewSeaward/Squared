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
            SceneManager.LoadScene(2);
        }

        public void Menu_Clicked()
        {
            SceneManager.LoadScene(1);
        }
    }
}
