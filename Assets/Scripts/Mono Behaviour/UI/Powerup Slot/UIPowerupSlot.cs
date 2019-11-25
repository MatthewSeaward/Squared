using System;

namespace Assets.Scripts.UI
{
    class UIPowerupSlot : PowerupSlot
    {
        protected override bool EnableButton(int remaining)
        {
            return true;
        }

        protected override void OnButtonClicked()
        {
            FindObjectOfType<PowerupTab>().SelectPowerup(this);
        }
    }
}
