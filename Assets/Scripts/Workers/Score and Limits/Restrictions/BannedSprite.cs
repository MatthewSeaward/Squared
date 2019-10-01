using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class BannedSprite : IRestriction
    {
        private readonly string Sprite;
        private bool failed = false;
        public int SpriteValue { get; private set; }

        public BannedSprite(int spriteValue)
        {
            Sprite = ((SquarePiece.Colour) spriteValue).ToString();
            SpriteValue = spriteValue;
        }

        public string GetRestrictionText()
        {
            return "Banned: " + Sprite;
        }

        public string GetRestrictionDescription() => GetRestrictionText();

        public bool ViolatedRestriction()
        {
            return failed;
        }

        public void Reset()
        {
            failed = false;
        }

        public void SequenceCompleted(ISquarePiece[] sequence)
        {
            foreach(var item in sequence)
            {
                if (item.Sprite.name == Sprite || item.Sprite.name == SpriteValue.ToString())
                {
                    failed = true;
                }
            }
        }

        public void Update(float deltaTime)
        {
           
        }
    }
}
