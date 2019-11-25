using Assets.Scripts.Workers.Helpers.Extensions;
using Assets.Scripts.Workers.IO.Data_Entities;
using UnityEngine;

namespace Assets.Scripts
{
    class PieceFallLerp : Lerp
    {
        [SerializeField]
        private GameObject effect;

        public override void Setup(Vector3 target)
        {
            speed = 15;
            effect.SetActive(true);
            ParticleSystem.MainModule settings = effect.GetComponent<ParticleSystem>().main;

            Color colour = Color.white;

            var sprite = GetComponent<SpriteRenderer>();
            if (sprite != null)
            {
                colour = sprite.sprite.texture.GetTextureColour();
            }

            settings.startColor = new ParticleSystem.MinMaxGradient(colour);

            base.Setup(target);
        }

        protected override void LerpComplete()
        {
            base.LerpComplete();
            effect.SetActive(false);
        }
    }
}
