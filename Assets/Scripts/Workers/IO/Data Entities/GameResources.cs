using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Data_Entities
{
    public static class GameResources
    {
        public static Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();
        public static Dictionary<string, GameObject> ParticleEffects = new Dictionary<string, GameObject>();
        public static Dictionary<string, GameObject> GameObjects = new Dictionary<string, GameObject>();
        public static Dictionary<string, Sprite> PieceSprites = new Dictionary<string, Sprite>();

        public static void PlayEffect(string name, Vector3 pos)
        {
            var obj = ParticleEffects[name];

            var effect = ObjectPool.GetGameObject(obj);
            effect.transform.position = pos;
            effect.SetActive(true);
        }

        public static void PlayEffect(string name, Vector3 pos, Color colour)
        {
            var obj = ParticleEffects[name];           

            var effect = ObjectPool.GetGameObject(obj);
            effect.transform.position = pos;
            effect.SetActive(true);

            ParticleSystem.MainModule settings = effect.GetComponent<ParticleSystem>().main;
            settings.startColor = new ParticleSystem.MinMaxGradient(colour);
        }
    }
}
