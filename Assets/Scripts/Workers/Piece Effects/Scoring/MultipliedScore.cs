using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Piece_Effects.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Workers.Piece_Effects
{
    public class MultipliedScore : IScoreable, ILayeredSprite
    {
        private readonly int _factor;

        public MultipliedScore(int factor)
        {
            _factor = factor;
        }

        public int ScorePiece(int currentScore)
        {
            return currentScore * _factor;
        }

        public Sprite GetSprite()
        {
            switch(_factor)
            {
                case 2:
                    return GameResources.Sprites["x2"];
                case 3:
                    return GameResources.Sprites["x3"];
                default:
                    return null;
            }
        }
    }
}
