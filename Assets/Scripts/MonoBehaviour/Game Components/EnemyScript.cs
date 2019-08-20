using Assets.Scripts.Workers.Enemy;
using UnityEngine;

namespace Assets.Scripts
{
    class EnemyScript : MonoBehaviour
    {
        [SerializeField]
        private EnemyInfo EnemyInfo;

        public int PiecesForRage => EnemyInfo.PiecesForRage;

        public IEnemyRage EnemyRage { get; private set; }
        public GameObject EnemyHead;

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
