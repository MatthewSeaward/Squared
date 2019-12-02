using Assets.Scripts.Workers.Data_Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class HeartDisplay : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponentInChildren<Text>().text = $"X{UserManager.LivesRemaining}";
        }
    }
}