using Assets.Scripts.Workers.IO.Data_Entities;
using UnityEngine;

namespace Assets.Scripts
{
    class StarPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject Star1, Star2, Star3;

        public void Configure(LevelProgress progress)
        {
            Star1.SetActive(progress?.StarAchieved >= 1);
            Star2.SetActive(progress?.StarAchieved >= 2);
            Star3.SetActive(progress?.StarAchieved >= 3);           
        }
    }
}
