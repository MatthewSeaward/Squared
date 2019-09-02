using System.Collections;
using UnityEngine;

public delegate void LerpCompleted();

namespace Assets.Scripts
{
    public class Lerp : MonoBehaviour
    {
        private Vector3 _start;
        public Vector3 Target = new Vector3 (0, float.MaxValue);
        private float journeyLength;
        private float startTime;
        private float speed = 10f;
        public event LerpCompleted LerpCompleted;

        private bool AtTarget => Vector3.Distance(transform.localPosition, Target) < 0.01f;

        public void Setup(Vector3 target)
        {
            _start = transform.position;
            Target = target;
            journeyLength = Vector3.Distance(_start, Target);
            startTime = Time.time;

            if (!AtTarget)
            {
                StartCoroutine(nameof(StartLerp));
            }

        }

        private IEnumerator StartLerp()
        {
            while (!AtTarget)
            {
                // Distance moved = time * speed.
                float distCovered = (Time.time - startTime) * speed;

                // Fraction of journey completed = current distance divided by total distance.
                float fracJourney = distCovered / journeyLength;

                transform.position = Vector3.Lerp(_start, Target, fracJourney);
                yield return new WaitForFixedUpdate();

            }

            LerpCompleted?.Invoke();
            yield return null;
        }
        
    }
}
