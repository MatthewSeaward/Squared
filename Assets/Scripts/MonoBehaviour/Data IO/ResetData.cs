using Assets.Scripts.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    class ResetData : MonoBehaviour
    {
        public void OnClick()
        {
            LevelManager.Instance.ResetSavedData();
            SceneManager.LoadScene(Scenes.Loading);
        }
    }
}
