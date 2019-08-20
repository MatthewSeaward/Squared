using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class ButtonLoadScene : MonoBehaviour
    {
        public void Button_OnClick(int scene)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
