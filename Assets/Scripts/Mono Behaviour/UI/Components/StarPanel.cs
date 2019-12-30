using UnityEngine;

namespace Assets.Scripts
{
    class StarPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject Star1, Star2, Star3;

        public void Configure(int StarAchieved)
        {
            Star1.SetActive(StarAchieved >= 1);
            Star2.SetActive(StarAchieved >= 2);
            Star3.SetActive(StarAchieved >= 3);           
        }
    }
}
