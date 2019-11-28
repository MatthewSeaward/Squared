using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.Powerups;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.On_Destroy
{
    class TreasureChest : IOnDestroy
    {
        private ISquarePiece squarePiece;

        public TreasureChest (ISquarePiece sqaurePiece)
        {
            this.squarePiece = sqaurePiece;
        }

        public void OnDestroy()
        {
            switch(Random.Range(0, 5))
            {
                case 1:
                    var powerups = UserPowerupManager.Instance.EquippedPowerups;
                    UserPowerupManager.Instance.AddNewPowerup(powerups[Random.Range(0, powerups.Length)]);
                    break;
                case 2:
                    var extraTime = new ExtraTime();
                    extraTime.Invoke();
                    break;
                case 3:
                    var extraPoints = new ExtraPoints();
                    extraPoints.Invoke();
                    break;
                default:
                    GameObject.FindObjectOfType<ScoreKeeper>().GainPoints(Random.Range(10, 20), new ISquarePiece[] { squarePiece });
                    break;

            }


        }
    }
}
