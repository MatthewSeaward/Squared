using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects
{
    public class SingleScore : IScoreable, ILayeredSprite
    {
        private int scoreValue;

        public SingleScore(int scoreValue = 1)
        {
            this.scoreValue = scoreValue;
        }

        public int ScorePiece(int currentScore)
        {
            return currentScore + scoreValue;
        }

        public Sprite GetSprite()
        {
            if (scoreValue > 1)
            {
                return GameResources.Sprites[scoreValue.ToString()];
            }
            else
            {
                return null;
            }
        }
    }
}
