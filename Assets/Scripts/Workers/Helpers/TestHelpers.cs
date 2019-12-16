using NSubstitute;
using System.Collections.Generic;
using UnityEngine;
using static PieceBuilderDirector;

namespace Assets.Scripts.Workers
{
    public static class TestHelpers
    {
        public static Dictionary<string, Sprite> CreatedSprites = new Dictionary<string, Sprite>();

        public static ISquarePiece CreatePiece()
        {
            return Substitute.For<ISquarePiece>();
        }

        public static ISquarePiece CreatePiece(PieceTypes type)
        {
            var piece =  Substitute.For<ISquarePiece>();
            piece.Type = type;
            return piece;
        }

        public static Sprite GetSprite(string name)
        {
            if (CreatedSprites.ContainsKey(name))
            {
                if (CreatedSprites[name] == null)
                {
                    CreatedSprites.Remove(name);
                }
                else
                {
                    return CreatedSprites[name];
                }
            }

           var sprite = Sprite.Create(new Texture2D(1, 1), new Rect(), new Vector2());
            sprite.name = name;

            CreatedSprites.Add(name, sprite);

            return sprite;
        }
    }
}
