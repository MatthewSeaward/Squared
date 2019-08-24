using UnityEngine;

namespace Assets.Scripts.Workers.Helpers.Extensions
{
    public static class TextureHelpers
    {
        public static Color GetTextureColour(this Texture2D texture)
        {
            return texture.GetPixel(40, texture.height / 2);
        }
    }
}
