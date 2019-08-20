using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Workers.IO.Data_Entities
{
    public static class GameResources
    {
        public static Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();
        public static Dictionary<string, GameObject> ParticleEffects = new Dictionary<string, GameObject>();

        public static void PlayEffect(string name, Vector3 pos)
        {
            PlayEffect(name, pos, Color.white);
        }

        public static void PlayEffect(string name, Vector3 pos, Color colour)
        {
            var obj = ParticleEffects[name];           

            var effect = ObjectPool.GetGameObject(obj);
            
            ParticleSystem.MainModule settings = effect.GetComponent<ParticleSystem>().main;
            settings.startColor = new ParticleSystem.MinMaxGradient(colour);

            GameObject.Instantiate(effect, pos, new Quaternion(0, 0, 0, 0));
        }
    }
}
