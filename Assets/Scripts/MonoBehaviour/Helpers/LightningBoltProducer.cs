using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LightningBoltProducer : MonoBehaviour
    {
        [SerializeField]
        private GameObject LightningBolt;

        private List<GameObject> currentBolts = new List<GameObject>();

        public static LightningBoltProducer Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void ProduceBolt(Vector3 position)
        {
            var newBolt = ObjectPool.Instantiate(LightningBolt, new Vector3(0, 0, 0));

            var line = newBolt.GetComponent<LineRenderer>();
            line.SetPositions(new Vector3[] { new Vector3(0, -5), position });

            currentBolts.Add(newBolt);
        }

        public void ClearBolts()
        {
            foreach(GameObject bolt in currentBolts)
            {
                bolt.SetActive(false);
            }
            currentBolts.Clear();
        }
    }
}
