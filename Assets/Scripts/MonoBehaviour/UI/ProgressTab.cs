using Assets.Scripts.Workers.Helpers;
using System;
using System.Collections.Generic;
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

            try
            {

                //for (int i = 0; i < progress.Length; i++)
                //{
                //    progress[i].Setup((Colour) i);

                //}
            }
            catch (Exception ex)
            {
                DebugLogger.Instance.WriteException(ex);
            }
        }
    }
}
