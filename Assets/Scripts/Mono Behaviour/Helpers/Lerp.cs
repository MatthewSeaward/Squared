using Assets.Scripts.Workers.Test_Mockers.Lerp;
using System.Collections;
using UnityEngine;

public delegate void LerpCompleted();

namespace Assets.Scripts
{
    public class Lerp : MonoBehaviour, ILerp
    {
        private Vector3 _start;
        public Vector3 Target = new Vector3 (0, float.MaxValue);
        private float journeyLength;
        private float startTime;
        protected float speed = 10f;
        public bool LerpInProgress { get; private set; }

        public event LerpCompleted LerpCompleted;

        private bool AtTarget => Vector3.Distance(transform.localPosition, Target) < 0.01f;

        public virtual void Setup(Vector3 target)
        {
            _start = transform.position;
            Target = target;
            journeyLength = Vector3.Distance(_start, Target);
            startTime = Time.time;
       
            if (!AtTarget && !LerpInProgress)
            {
                StartCoroutine(nameof(StartLerp));
            }

        }

        private IEnumerator StartLerp()
        {
            LerpInProgress = true;
            while (!AtTarget)
            {
                // Distance moved = time * speed.
                float distCovered = (Time.time - startTime) * speed;

                // Fraction of journey completed = current distance divided by total distance.
                float fracJourney = distCovered / journeyLength;

                transform.position = Vector3.Lerp(_start, Target, fracJourney);
                yield return new WaitForFixedUpdate();

            }

            LerpComplete();
            yield return null;
        }

        protected virtual void LerpComplete()
        {
            LerpCompleted?.Invoke();
            LerpInProgress = false;
        }
        
    }
}
