using Assets.Scripts.Workers.Generic_Interfaces;
using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Mono_Behaviour.UI.Components
{
    public class RestrictionInfo : MonoBehaviour
    {
        [SerializeField]
        private Text Text;

        [SerializeField]
        private Image Image;

        public bool ShowLiveInfo = false;

        public void DisplayRestriction(IRestriction restriction)
        {
            var restrictionSprite = restriction as IHasSprite;

            if (restrictionSprite != null)
            {
                DisplayRestrictionTextAndImage(restriction, restrictionSprite);
            }
            else
            {
                DisplayRestrictionText(restriction);
            }
        }

        private void DisplayRestrictionText(IRestriction restriction)
        {
            Image.gameObject.SetActive(false);

            Text.text = ShowLiveInfo ? restriction.GetRestrictionText() : restriction.GetRestrictionDescription() ;
        }

        private void DisplayRestrictionTextAndImage(IRestriction restriction, IHasSprite restrictionSprite)
        {
            Image.gameObject.SetActive(true);

            Text.text = ShowLiveInfo ? restriction.GetRestrictionText() : restriction.GetRestrictionDescription();
            Image.sprite = restrictionSprite.GetSprite();
        }
    }
}
