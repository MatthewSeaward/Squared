using Assets.Scripts.Workers.Managers;
using UnityEngine;

namespace Assets.Scripts
{
    class DebugControls : MonoBehaviour
    {
        private void Awake()
        {
            gameObject.SetActive(Debug.isDebugBuild);
        }

        public void AddLife()
        {
            LivesManager.Instance.GainALife();
        }

        public void LoseLife()
        {
            LivesManager.Instance.LoseALife();
        }
    }
}
