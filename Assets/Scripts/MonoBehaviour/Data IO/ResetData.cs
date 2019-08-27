using Assets.Scripts.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class ResetData : MonoBehaviour
    {
        public void OnClick()
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(Scenes.Loading);
        }
    }
}
