using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO.Data_Entities;
using System;
using System.Linq;
using UnityEngine;
using static SquarePiece;

namespace Assets.Scripts.UI
{
    class ProgressTab : MonoBehaviour
    {
        void OnEnable()
        {
            var progress = FindObjectsOfType<PieceCollectionProgress>();

            foreach(Colour type in Enum.GetValues(typeof(Colour)))
            {
                if (type == Colour.None)
                {
                    continue;
                }

                progress[(int) type].Setup(type);
            }
        }
    }
}
