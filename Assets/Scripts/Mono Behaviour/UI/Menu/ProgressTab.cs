using UnityEngine;

namespace Assets.Scripts
{
    class ProgressTab : MonoBehaviour
    {
        private void OnEnable()
        {
            var collectionProgress = FindObjectsOfType<PieceCollectionProgress>();

            foreach (var collection in collectionProgress)
            {
                collection.Setup();
            }
        }
    }
}
