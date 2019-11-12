using Assets.Scripts.Workers.Enemy;
using Assets.Scripts.Workers.Grid_Management;
using GridGeneration;
using UnityEngine;

namespace Assets.Scripts
{
    class AvailableMoveChecker : MonoBehaviour
    {
        DestroyRage rage = new DestroyRage();
            
        private void Start()
        {
            PieceDropper.BoardRefreshed += CheckForMoves;
            BasicGridGenerator.GridGenerated += CheckForMoves;

            rage.SelectionAmount = 5;                       
        }

        private void OnDestroy()
        {
            PieceDropper.BoardRefreshed -= CheckForMoves;
            BasicGridGenerator.GridGenerated -= CheckForMoves;
        }

        private void CheckForMoves()
        {
            if (!MoveChecker.AvailableMove())
            {
                rage.InvokeRage();
            }
        }
    }
}
