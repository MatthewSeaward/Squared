using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class PauseIcon : MonoBehaviour
    {
        private void Awake()
        {
            GameManager.PauseStateChanged += PauseStateChanged;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            GameManager.PauseStateChanged -= PauseStateChanged;

        }

        private void PauseStateChanged(bool gamePaused)
        {
            gameObject.SetActive(gamePaused && !MenuProvider.Instance.OnDisplay);
        }
    }
}
