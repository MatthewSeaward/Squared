using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    class PowerupSlot : MonoBehaviour
    {
        [SerializeField]
        private Image Icon;

        [SerializeField]
        private Button button;

        private IPowerup powerup;

        public void Setup(IPowerup powerup)
        {
            this.powerup = powerup;
            Icon.sprite = powerup.Icon;
            button.onClick.AddListener(() => powerup.Invoke());
        }

        private void Update()
        {
            powerup.Update(Time.deltaTime);
        }
    }
}
