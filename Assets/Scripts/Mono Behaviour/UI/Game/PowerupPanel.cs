using Assets.Scripts.Workers.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class PowerupPanel : MonoBehaviour
    {
        private void Start()
        {
            if (LevelManager.Instance.DailyChallenge)
            {
                gameObject.SetActive(false);
                return;
            }

            PieceSelectionManager.SelectedPiecesChanged += PieceSelectionManager_SelectedPiecesChanged;

            var buttons = GetComponentsInChildren<PowerupSlot>();

            buttons[0].Setup(UserPowerupManager.Instance.EquippedPowerups[0]);
            buttons[1].Setup (UserPowerupManager.Instance.EquippedPowerups[1]);
            buttons[2].Setup(UserPowerupManager.Instance.EquippedPowerups[2]);
        }     

        private void OnDestroy()
        {
            PieceSelectionManager.SelectedPiecesChanged -= PieceSelectionManager_SelectedPiecesChanged;
        }

        private void PieceSelectionManager_SelectedPiecesChanged(LinkedList<ISquarePiece> pieces)
        {
            gameObject.SetActive(PieceSelectionManager.Instance.CurrentPieces.Count < 2);
        }
    }
}