using Assets.Scripts.Workers.Managers;
using static SquarePiece;

namespace Assets.Scripts.Workers.Enemy
{
    public class RemoveSpriteEvent : IEnemyRage
    {
        public Colour ColourToRemove { private set; get; }

        public RemoveSpriteEvent(Colour newColour)
        {
            ColourToRemove = newColour;
        }

        public bool CanBeUsed()
        {
            return LevelManager.Instance.SelectedLevel.Colours.Contains(ColourToRemove);
        }

        public void InvokeRage()
        {
            LevelManager.Instance.SelectedLevel.Colours.Remove(ColourToRemove);
        }

        public void Update(float deltaTime)
        {

        }
    }
}
