using Assets.Scripts.UI;
using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.Powerups.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class PowerupTab : MonoBehaviour
    {
        [SerializeField]
        private GameObject PowerupIcon;

        [SerializeField]
        private Text nameText;

        [SerializeField]
        private Text descriptionText;

        [SerializeField]
        private Transform Grid;

        [SerializeField]
        private PowerupSlot[] selectedPowerups;

        [SerializeField]
        private Button[] EquipButtons;

        private PowerupSlot currentSelected;

        internal void SelectPowerup(PowerupSlot slot)
        {
            if (currentSelected != null)
            {
                currentSelected.button.interactable = true;
            }

            nameText.text = slot.powerup.Name;
            descriptionText.text = slot.powerup.Description;
            slot.button.interactable = false;

            currentSelected = slot;

            SetEquipButtons(!UserPowerupManager.Instance.PowerupEquipped(slot.powerup));
        }

        private void OnEnable()
        {
            SetupAvaiblePowerups();
            SetupSelectedPowerups();
        }

        private void SetEquipButtons(bool enable)
        {
            foreach(var b in EquipButtons)
            {
                b.gameObject.SetActive(enable);
            }
        }

        private void SetupAvaiblePowerups()
        {
            for (int i = 0; i < Grid.childCount; i++)
            {
                Grid.GetChild(i).gameObject.SetActive(false);
            }

            PowerupSlot first = null;
            foreach (IPowerup powerup in PowerupFactory.GetAllPowerups())
            {
                var obj = ObjectPool.Instantiate(PowerupIcon, Vector3.zero);
                obj.transform.localScale = new Vector3(1, 1, 1);

                obj.transform.SetParent(Grid);

                obj.GetComponent<PowerupSlot>().Setup(powerup);

                if (first == null)
                {
                    first = obj.GetComponent<PowerupSlot>();
                }
            }

            SelectPowerup(first);
        }

        private void SetupSelectedPowerups()
        {
            for (int i = 0; i < selectedPowerups.Length; i++)
            {
                selectedPowerups[i].Setup(UserPowerupManager.Instance.EquippedPowerups[i]);
            }
        }

        public void EquipPowerup(int slot)
        {
            UserPowerupManager.Instance.EquipPowerup(currentSelected.powerup, slot);
            OnEnable();
        }
    }
}
