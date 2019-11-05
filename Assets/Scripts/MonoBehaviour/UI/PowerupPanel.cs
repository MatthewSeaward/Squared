using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.Powerups;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    class PowerupPanel : MonoBehaviour
    {
        private void Start()
        {
            PieceSelectionManager.SelectedPiecesChanged += PieceSelectionManager_SelectedPiecesChanged;

            var buttons = GetComponentsInChildren<PowerupSlot>();

            buttons[0].Setup(UserPowerupManager.Instance.SelectedPowerups[0]);
            buttons[1].Setup (UserPowerupManager.Instance.SelectedPowerups[1]);
            buttons[2].Setup(UserPowerupManager.Instance.SelectedPowerups[2]);
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