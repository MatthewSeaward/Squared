using Assets.Scripts.Workers.IO.Data_Entities;
using UnityEngine;

namespace Assets.Scripts
{
    class ResourceLoader : MonoBehaviour
    {
        private void Awake()
        {
            GameResources.Sprites.Clear();

            var resources = Resources.LoadAll<Sprite>("Graphics/Sprites");
            foreach(var item in resources)
            {
                GameResources.Sprites.Add(item.name, item); 
            }

            GameResources.PieceSprites.Clear();

            var squares = Resources.LoadAll<Sprite>("Graphics/Squares");
            foreach (var item in squares)
            {
                GameResources.PieceSprites.Add(item.name, item);
            }

            GameResources.ParticleEffects.Clear();                        
            foreach (var item in Resources.LoadAll<GameObject>("Particle Effects"))
            {
                GameResources.ParticleEffects.Add(item.name, item);
            }

            GameResources.GameObjects.Clear();
            foreach (var item in Resources.LoadAll<GameObject>("Game Objects"))
            {
                GameResources.GameObjects.Add(item.name, item);
            }
        }
    }
}
