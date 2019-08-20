using Assets.Scripts.Workers.Score_and_Limits.Interfaces;
using System.Collections.Generic;

namespace Assets.Scripts.Workers.Score_and_Limits
{
    public class BannedSprite : IRestriction
    {
        private string Sprite;
        private bool failed = false;
        private readonly int SpriteValue;

        public BannedSprite(int spriteValue)
        {
            Sprite = ((SquarePiece.Colour) spriteValue).ToString();
            SpriteValue = spriteValue;
        }

        public BannedSprite(string sprite)
        {
            Sprite = sprite;
        }

        public string GetDescription()
        {
            return $"{Sprite} pieces are banned";
        }

        public string GetRestrictionText()
        {
            return $"Banned: {Sprite}";
        }

        public bool ViolatedRestriction()
        {
            return failed;
        }

        public void Reset()
        {
            failed = false;
        }

        public void SequenceCompleted(LinkedList<ISquarePiece> sequence)
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
