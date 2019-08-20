using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class GameStats : MonoBehaviour
    {
        [SerializeField]
        private Text TotalStars;

        private void Start()
        {
            TotalStars.text = "X" + LevelManager.Instance.CurrentStars;
        }
    }
}
