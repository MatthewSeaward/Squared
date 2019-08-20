using Assets.Scripts.Workers.IO.Data_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    class ResourceLoader : MonoBehaviour
    {
        private void Awake()
        {
            GameResources.Sprites.Clear();

            var resources = Resources.LoadAll<Sprite>("Graphics/Piece Layers");
            foreach(var item in resources)
            {
                GameResources.Sprites.Add(item.name, item); 
            }


            GameResources.ParticleEffects.Clear();                        
            foreach (var item in Resources.LoadAll<GameObject>("Particle Effects"))
            {
                GameResources.ParticleEffects.Add(item.name, item);
            }
        }
    }
}
