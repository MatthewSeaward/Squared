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
            ObjectPool.PreLoad(GameResources.GameObjects["PieceSlot"], 50);
            ObjectPool.PreLoad(GameResources.GameObjects["Landing"], 50);
            ObjectPool.PreLoad(GameResources.GameObjects["PieceDestroy"], 50);

            ObjectPool.PreLoad(GameResources.GameObjects["LightningBolt"], 10);


        }

    }
}
