using Assets.Scripts.Workers.Managers;
using System.Linq;
using static SquarePiece;

namespace Assets.Scripts.Workers.Enemy.Events
{
    public class AddSpriteEvent : IEnemyRage
    {
        public Colour NewColour { private set; get; }

        public AddSpriteEvent(Colour newColour)
        {
            this.NewColour = newColour;
        }

        public bool CanBeUsed()
        {
            return !LevelManager.Instance.SelectedLevel.Colours.Contains(NewColour); 
        }

        public void InvokeRage()
        {
            LevelManager.Instance.SelectedLevel.Colours.Add(NewColour);
        }

        public void Update(float deltaTime)
        {

        }
    }
}
