using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO.Data_Entities;
using System;
using System.Linq;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.UI
{
    class ProgressTab : MonoBehaviour
    {
        void OnEnable()
        {
            var progress = FindObjectsOfType<PieceCollectionProgress>();

            foreach(Colour type in Enum.GetValues(typeof(Colour)))
            {
                if (type == Colour.None)
                {
                    continue;
                }

                var currentProgress = progress[(int) type];

                currentProgress.Image.sprite = GameResources.PieceSprites[((int) type).ToString()];

                var collected = PieceCollectionManager.Instance.PiecesCollected.Pieces.FirstOrDefault(x => x.PieceColour == type);

                int piecesCollected = collected != null ? collected.Count : 0;
                int increment = 50;

                var multiplier = (piecesCollected / increment) + 1;

                currentProgress.ProgressBar.UpdateProgressBar(piecesCollected, increment * multiplier, true);
            }
        }
    }
}
