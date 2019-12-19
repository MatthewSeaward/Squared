using Assets.Scripts.Workers.Factorys;
using Assets.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.Powerups.Interfaces;
using System;
using UnityEngine;
using UnityEngine.UI;
using static SquarePiece;

namespace Assets.Scripts
{
    class PowerupCollectionInfo : MonoBehaviour
    {
        public void Setup(IPowerup powerup)
        {
            Colour colour = Colour.None;
            foreach(Colour c in Enum.GetValues(typeof(Colour)))
            {
                if (powerup.GetType() == PowerupFactory.GetPowerup(c).GetType())
                {
                    colour = c;
                    break;
                }
            }

            GetComponentInChildren<Image>().sprite = GameResources.PieceSprites[((int)colour).ToString()];

        }
    }
}
