using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects.On_Destroy
{
    class BonusPoints : IOnDestroy
    {
        private ISquarePiece squarePiece;

        public BonusPoints (ISquarePiece sqaurePiece)
        {
            this.squarePiece = sqaurePiece;
        }

        public void OnDestroy()
        {          
           GameObject.FindObjectOfType<ScoreKeeper>().GainPoints(Random.Range(10, 20), new ISquarePiece[] { squarePiece });
        }
    }
}