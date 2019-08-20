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
        bool start;
        private float timer;
        public event LerpCompleted LerpCompleted;

        public void Setup(Vector3 target)
        {
            _start = transform.position;
            Target = target;
            journeyLength = Vector3.Distance(_start, Target);
            startTime = Time.time;

            start = true;

        }

        private void Update()
        {
            if (!start)
            {
                return;
            }

            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(_start, Target, fracJourney);

            if (transform.position == Target)
            {
                LerpCompleted?.Invoke();
                start = false;
            }
        }
    }
}
