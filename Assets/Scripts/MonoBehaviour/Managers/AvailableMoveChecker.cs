using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Enemy;
using GridGeneration;
using UnityEngine;

namespace Assets.Scripts
{
    class AvailableMoveChecker : MonoBehaviour
    {
        DestroyRage rage = new DestroyRage(Vector3.zero);
            

        private void Start()
        {
            PieceDropper.BoardRefreshed += CheckForMoves;
            BasicGridGenerator.GridGenerated += CheckForMoves;

            rage.AmountToDestroy = 5;
                       
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
