using UnityEngine;

namespace Assets.Scripts.Workers.Test_Mockers.Lerp
{
    public class LerpNotInProgress : MonoBehaviour, ILerp
    {
        public bool LerpInProgress => false;
    }
}
