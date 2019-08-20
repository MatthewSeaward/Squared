using UnityEngine;

namespace Assets.Scripts
{
    class BackgroundManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] Backgrounds;

        private void Start()
        {
            foreach(var background in Backgrounds)
            {
               background.SetActive(background.name == LevelManager.Instance.SelectedChapter);

            }
        }
    }
}
