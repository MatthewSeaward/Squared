using Assets.Scripts.Workers.Data_Managers;
using System;
using UnityEngine;

namespace Assets.Scripts
{
    class InGamePieceCollectionManager:MonoBehaviour
    {
        [SerializeField]
        private GameObject PieceProgress;

  
        private void Awake()
        {
            PieceCollectionManager.PiecesCollectedEvent += PieceCollectionManager_PiecesCollectedEvent;
        }

        private void OnDestroy()
        {
            PieceCollectionManager.PiecesCollectedEvent -= PieceCollectionManager_PiecesCollectedEvent;
        }

        private void PieceCollectionManager_PiecesCollectedEvent(SquarePiece.Colour type, int previous, int gained)
        {
            if (type == SquarePiece.Colour.None)
            {
                return;
            }

            var obj = ObjectPool.Instantiate(PieceProgress, Vector3.zero);
            obj.transform.SetParent(this.transform);
            obj.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            obj.GetComponent<PieceCollectionProgress>().Setup(type, previous, gained);
            var time = obj.AddComponent<DestroyAfterTime>();
            time.Setup(Constants.GameSettings.InGamePieceCollectedShowTime);
        }                
    }
}
