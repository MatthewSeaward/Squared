using UnityEngine;

namespace Assets.Scripts
{
    class DisableWhenDone : MonoBehaviour
    {
        void Update()
        {
            if (!GetComponent<ParticleSystem>().isPlaying)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
