using Assets.Scripts.Constants;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.IO.Data_Entities;
using UnityEngine;

namespace Assets.Scripts
{
    class StarShooter : MonoBehaviour
    {
        [SerializeField]
        private Vector2 Min;

        [SerializeField]
        private Vector2 Max;

        private void Awake()
        {
            ScoreKeeper.GameCompleted += ScoreKeeper_GameCompleted;
        }

        private void OnDestroy()
        {
            ScoreKeeper.GameCompleted -= ScoreKeeper_GameCompleted;
        }

        private void ScoreKeeper_GameCompleted(string chapter, int level, int star, int score, GameResult result, bool dailyChallenge)
        {
            if (result == GameResult.ReachedTarget)
            {
                InvokeRepeating(nameof(ShootStar), 0f, GameSettings.StarShooterFreq);
            }
        }

        private void ShootStar()
        {
            var position = new Vector3(Random.Range(Min.x, Max.x), Random.Range(Min.y, Max.y));

            ObjectPool.Instantiate(GameResources.ParticleEffects["Star Shot"], position); 
        }
    }
}
