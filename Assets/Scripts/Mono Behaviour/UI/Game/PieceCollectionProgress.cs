using Assets.Scripts.UI;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
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
        private PowerupSlot PowerupSlot;

        [SerializeField]
        public Colour colour;

        public IPowerup Powerup; 

        public void Setup()
        {
            Setup(colour);
        }

        public void Setup(Colour pieceColour)
        {
            PowerupSlot = GetComponentInChildren<PowerupSlot>();

            SetupSprite(pieceColour);

            var collected = PieceCollectionManager.Instance.PiecesCollected.Pieces.FirstOrDefault(x => x.PieceColour == pieceColour);
            SetupProgressBar(pieceColour, collected != null ? collected.Count : 0);
        }        

        public void Setup(Colour pieceColour, int previous, int gained)
        {
            PowerupSlot = GetComponentInChildren<PowerupSlot>();

            SetupSprite(pieceColour);
            SetupProgressBar(pieceColour, previous);
            StartCoroutine(IncrementOverTime(pieceColour, previous, gained));
        }

        private void SetupSprite(Colour pieceColour)
        {
            this.colour = pieceColour;
            Image.sprite = GameResources.PieceSprites[((int)pieceColour).ToString()];

            Powerup = PowerupFactory.GetPowerup(pieceColour);
            PowerupSlot.Setup(Powerup, false);
        }

        private void SetupProgressBar(Colour pieceColour, int piecesCollected)
        {
            int increment = RemoteConfigHelper.GetCollectionInterval(pieceColour);

            var multiplier = (piecesCollected / increment) + 1;

            ProgressBar.UpdateProgressBar(piecesCollected % increment, increment, $"{piecesCollected}/{(increment * multiplier)}");

            if (piecesCollected % increment == 0)
            {
                GameResources.PlayEffect("Powerup Unlocked", Camera.main.ScreenToWorldPoint(PowerupSlot.transform.position));
            }
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

        internal void AddOnClick(Action action)
        {
            var button = PowerupSlot.GetComponent<Button>() != null ? PowerupSlot.GetComponent<Button>() : PowerupSlot.gameObject.AddComponent<Button>();

            button.onClick.AddListener(() => action());
        }
    }
}
