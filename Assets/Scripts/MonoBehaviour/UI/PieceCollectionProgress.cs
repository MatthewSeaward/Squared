﻿using Assets.Scripts.UI;
using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO.Data_Entities;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static SquarePiece;

namespace Assets.Scripts
{
    class PieceCollectionProgress : MonoBehaviour
    {
        public Image Image;
        public ProgressBar ProgressBar;
        public Image Powerup;

        public void Setup(Colour pieceColour)
        {
            SetupSprite(pieceColour);

            var collected = PieceCollectionManager.Instance.PiecesCollected.Pieces.FirstOrDefault(x => x.PieceColour == pieceColour);
            SetupProgressBar(pieceColour, collected != null ? collected.Count : 0);
        }        

        public void Setup(Colour pieceColour, int previous, int gained)
        {
            SetupSprite(pieceColour);
            SetupProgressBar(pieceColour, previous);
            StartCoroutine(IncrementOverTime(pieceColour, previous, gained));
        }

        private void SetupSprite(Colour pieceColour)
        {
            Image.sprite = GameResources.PieceSprites[((int)pieceColour).ToString()];
        }

        private void SetupProgressBar(Colour pieceColour, int piecesCollected)
        {
            int increment = RemoteConfigHelper.GetCollectionInterval(pieceColour);

            var multiplier = (piecesCollected / increment) + 1;

            ProgressBar.UpdateProgressBar(piecesCollected % increment, increment, $"{piecesCollected}/{(increment * multiplier)}");
        }

        IEnumerator IncrementOverTime(Colour pieceColour, int previous, int gained)
        {
            int currentTotal = previous;

            while (currentTotal != previous + gained)
            {
                currentTotal++;
                SetupProgressBar(pieceColour, currentTotal);
                yield return new WaitForSeconds(Constants.GameSettings.PieceIncrementSpeed);
            }
        }
    }
}
