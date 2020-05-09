using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.Interfaces
{
    public class LayeredSpriteSettings
    {
        public Sprite[] Sprites { get; set; }

        public int OrderInLayer { get; set; } = 5;
    }
}
