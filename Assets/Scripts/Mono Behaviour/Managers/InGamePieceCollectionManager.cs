using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static SquarePiece;

namespace Assets.Scripts
{
    class InGamePieceCollectionManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject PieceProgress;

        private Dictionary<Colour, InGamePieceCollectionProgress> pieceCollectionInProgress = new Dictionary<Colour, InGamePieceCollectionProgress>();

        private void Awake()
        {
            PieceCollectionManager.PiecesCollectedEvent += PieceCollectionManager_PiecesCollectedEvent;
            ScoreKeeper.GameCompleted += ShowPiecesCollected;
        }

        private void OnDestroy()
        {
            PieceCollectionManager.PiecesCollectedEvent -= PieceCollectionManager_PiecesCollectedEvent;
            ScoreKeeper.GameCompleted -= ShowPiecesCollected;

        }

        private void PieceCollectionManager_PiecesCollectedEvent(Colour colour, int previous, int gained)
        {
            if (colour == Colour.None)
            {
                return;
            }

            if (!pieceCollectionInProgress.ContainsKey(colour))
            {
                pieceCollectionInProgress.Add(colour, new InGamePieceCollectionProgress() { Gained = gained, Previous = previous });
            }
            else
            {
                pieceCollectionInProgress[colour].Gained += gained;
            }

        }

        private void ShowPiecesCollected(string chapter, int level, int star, int score, GameResult result, bool dailyChallenge)
        {
            foreach (var ingameProgress in pieceCollectionInProgress)
            {
                GameObject obj = null;

                var currentProgressBars = ObjectPool.GetActivePool(PieceProgress);
                if (currentProgressBars != null)
                {
                    obj = currentProgressBars.FirstOrDefault(x => x.GetComponent<PieceCollectionProgress>().colour == ingameProgress.Key);
                }

                if (obj == null)
                {
                    obj = ObjectPool.Instantiate(PieceProgress, Vector3.zero);
                }

                obj.transform.SetParent(this.transform);
                obj.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
                obj.GetComponent<PieceCollectionProgress>().Setup(ingameProgress.Key, ingameProgress.Value.Previous, ingameProgress.Value.Gained);
                obj.GetComponentInChildren<Button>().enabled = false;

                var time = obj.AddComponent<DestroyAfterTime>();
                time.Setup(Constants.GameSettings.InGamePieceCollectedShowTime);
            }
        }                
    }
}
