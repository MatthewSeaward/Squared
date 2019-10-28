﻿using Assets.Scripts.Constants;
using Assets.Scripts.Workers.IO.Data_Entities;
using UnityEngine;

namespace Assets.Scripts
{
    class PreLoader : MonoBehaviour
    {
        private void Awake()
        {
            ObjectPool.PreLoad(GameResources.GameObjects["Piece"], 50);
            ObjectPool.PreLoad(GameResources.GameObjects["PieceLayer"], 50);
            ObjectPool.PreLoad(GameResources.GameObjects["LightningBolt"], 10);            
            ObjectPool.PreLoad(GameResources.GameObjects["Animated Text"], 5);

            ObjectPool.PreLoad(GameResources.ParticleEffects["Landing"], 50);
            ObjectPool.PreLoad(GameResources.ParticleEffects["Piece Destroy"], 50);
            ObjectPool.PreLoad(GameResources.ParticleEffects["Star Shot"], 5);
            ObjectPool.PreLoad(GameResources.ParticleEffects["Score Shot 1"], 2);
            ObjectPool.PreLoad(GameResources.ParticleEffects["Score Shot 2"], 2);
            ObjectPool.PreLoad(GameResources.ParticleEffects["Score Shot 3"], 2);
            ObjectPool.PreLoad(GameResources.ParticleEffects["Piece Change"], 10);
            ObjectPool.PreLoad(GameResources.ParticleEffects["Powerup Unlocked"], 2);

            BonusPoints.Setup();
        }
    }
}
