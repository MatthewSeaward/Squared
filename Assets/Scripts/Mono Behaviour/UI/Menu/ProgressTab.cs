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
                collection.AddOnClick(() => ShowPowerup(collection));
            }
        }

        private void ShowPowerup(PieceCollectionProgress collection)
        {
            PowerupTab.SelectedPowerup = collection.Powerup;
            FindObjectOfType<MainMenu>().PoweurpsTab_Clicked();
        }
    }
}
