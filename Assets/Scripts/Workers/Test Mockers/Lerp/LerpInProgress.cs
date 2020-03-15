using UnityEngine;

namespace Assets.Scripts.Workers.Test_Mockers.Lerp
{
    public class LerpInProgress : MonoBehaviour, ILerp
    {
        bool ILerp.LerpInProgress => true;
    }
}
