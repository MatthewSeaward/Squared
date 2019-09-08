using Assets.Scripts.Constants;
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
            ScoreKeeper.GameVictory += ScoreKeeper_GameVictory;
        }

        private void OnDestroy()
        {
            ScoreKeeper.GameVictory -= ScoreKeeper_GameVictory;
        }

        private void ScoreKeeper_GameVictory()
        {
            InvokeRepeating(nameof(ShootStar), 0f, GameSettings.StarShooterFreq);
        }

        private void ShootStar()
        {
            var position = new Vector3(Random.Range(Min.x, Max.x), Random.Range(Min.y, Max.y));

            ObjectPool.Instantiate(GameResources.ParticleEffects["Star Shot"], position); 
        }
    }
}
