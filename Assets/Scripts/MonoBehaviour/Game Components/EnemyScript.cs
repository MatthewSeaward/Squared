using Assets.Scripts.Workers.Enemy;
using UnityEngine;

namespace Assets.Scripts
{
    class EnemyScript : MonoBehaviour
    {
        [SerializeField]
        private EnemyInfo EnemyInfo;

        [SerializeField]
        public GameObject EnemyHead;

        public int PiecesForRage => EnemyInfo.PiecesForRage;

        public IEnemyRage EnemyRage { get; private set; }

        private void Awake()
        {
            EnemyRage = EnemyInfo.GetRage(transform.position);
        }

        private void Update()
        {
            EnemyRage.Update(Time.deltaTime);
        }
    }
}
