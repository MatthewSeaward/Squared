using Assets.Scripts.Constants;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public delegate void ResetAllData();

    class ResetData : MonoBehaviour
    {
        public static ResetAllData ResetAllData;

        public void OnClick()
        {
            ResetAllData?.Invoke();

            SceneManager.LoadScene(Scenes.Loading);
        }
    }
}
