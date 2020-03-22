using Assets.Scripts.Workers.Helpers.Test_Helpers;
using NSubstitute;
using System.Collections.Generic;
using UnityEngine;
using static Assets.Scripts.Workers.Factorys.PieceBuilderDirector;
using static SquarePiece;

namespace Assets.Scripts.Workers.Helpers
{
    public static class TestHelpers
    {
        public static Dictionary<string, Sprite> CreatedSprites = new Dictionary<string, Sprite>();

        public static ISquarePiece CreatePiece()
        {
            return Substitute.For<ISquarePiece>();
        }

        public static ISquarePiece CreatePiece(int x, int y)
        {
            var piece =  Substitute.For<ISquarePiece>();
            piece.Position = new Vector2Int(x, y);
            return piece;
        }

        public static ISquarePiece CreatePiece(int x, int y, Colour colour)
        {
            var piece = Substitute.For<ISquarePiece>();
            piece.Position = new Vector2Int(x, y);

            piece.Sprite = GetSprite(colour);
            piece.PieceColour = colour;
            return piece;
        }

        public static ISquarePiece CreatePiece(int x, int y, bool active)
        {
            ISquarePiece piece;

            if (active)
            {
                piece = new DummyActiveSquarePiece();
            }
            else
            {
                piece = new DummyInactiveSquarePiece();
            }

            piece.Position = new Vector2Int(x, y);
            return piece;
        }

        public static ISquarePiece CreatePiece(PieceTypes type)
        {
            var piece =  Substitute.For<ISquarePiece>();
            piece.Type = type;
            return piece;
        }

        public static Sprite GetSprite(Colour colour)
        {
            var c = (int)colour;

            return GetSprite(c.ToString());
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
