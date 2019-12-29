using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

            GameObject obj = null;

            var currentProgressBars = ObjectPool.GetActivePool(PieceProgress);
            if (currentProgressBars != null)
            {
                obj = currentProgressBars.FirstOrDefault(x => x.GetComponent<PieceCollectionProgress>().colour == type);
            }
            if (obj == null)
            {
                obj = ObjectPool.Instantiate(PieceProgress, Vector3.zero);
            }

            obj.transform.SetParent(this.transform);
            obj.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            obj.GetComponent<PieceCollectionProgress>().Setup(type, previous, gained);
            obj.GetComponentInChildren<Button>().enabled = false;

            var time = obj.AddComponent<DestroyAfterTime>();
            time.Setup(Constants.GameSettings.InGamePieceCollectedShowTime);
        }                
    }
}
