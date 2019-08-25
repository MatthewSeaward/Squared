using NSubstitute;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers
{
    public static class TestHelpers
    {
        private static Dictionary<string, Sprite> CreatedSprites = new Dictionary<string, Sprite>();

        public static ISquarePiece CreatePiece()
        {
            return Substitute.For<ISquarePiece>();
        }

        public static Sprite GetSprite(string name)
        {
            if (CreatedSprites.ContainsKey(name))
            {
                return CreatedSprites[name];
            }

           var sprite = Sprite.Create(new Texture2D(1, 1), new Rect(), new Vector2());
            sprite.name = name;

            CreatedSprites.Add(name, sprite);

            return sprite;
        }
    }
}
