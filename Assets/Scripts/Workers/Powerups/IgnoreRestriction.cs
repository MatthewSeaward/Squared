using Assets.Scripts.Managers;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Powerups.Interfaces;
using Assets.Scripts.Workers.Score_and_Limits;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    class IgnoreRestriction : IPowerup
    {
        public Sprite Icon => GameResources.Sprites["Shield"];

        public string Name => "Ignore Restriction";

        public string Description => "Ignore the restriction for this turn.";

        public bool Enabled
        {
            get
            {
                return LevelManager.Instance.SelectedLevel.GetCurrentRestriction().GetType() != typeof(NoRestriction);
            }
        }

        public void Invoke()
        {
            GameManager.Instance.Restriction.Ignore();
            GameObject.FindObjectOfType<RestrictionDisplay>().ActivateRestriction();
        }

        public void MoveCompleted()
        {
        }

        public void Update(float deltaTime)
        {
        }
    }
}
