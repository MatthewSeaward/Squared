using UnityEngine;

namespace Assets.Scripts
{
    class DestroyAfterTime : MonoBehaviour
    {
        public void Setup(float time)
        {
            Invoke(nameof(DestroyThis), time);
        }

        private void DestroyThis()
        {
            gameObject.SetActive(false);
        }
    }
}
