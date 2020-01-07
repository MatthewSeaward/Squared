using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using Assets.Scripts.Workers.Score_and_Limits;
using UnityEngine;

namespace Assets.Scripts.Workers.Powerups
{
    public delegate void BonusAdded(ScoreBonus bonus);

    class ExtraPoints : IPowerup
    {
        public static BonusAdded BonusAdded;

        public Sprite Icon  => GameResources.Sprites["Trophy"];
        
        public string Name => "Extra Points";

        public string Description => "Increases the amount of points you earn for your next turn.";

        public bool Enabled => true;

        public void Invoke()
        {
            BonusAdded?.Invoke(new ScoreBonus(1.5f, 1));
        }

        public void MoveCompleted()
        {

        }
    }
}
