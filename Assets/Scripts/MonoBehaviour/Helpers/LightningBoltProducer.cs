using Assets.Scripts.Workers.IO.Data_Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class LightningBoltProducer : MonoBehaviour
    {
        private Dictionary<ISquarePiece, GameObject> currentBolts = new Dictionary<ISquarePiece, GameObject>();

        public static LightningBoltProducer Instance;

        private void Awake()
        {
            Instance = this;
        }

        public void ProduceBolt(ISquarePiece piece, Vector3 position)
        {
            GameObject bolt = null;

            if (currentBolts.ContainsKey(piece))
            {
                bolt = currentBolts[piece];
            }
            else
            {
                bolt = ObjectPool.Instantiate(GameResources.GameObjects["LightningBolt"], new Vector3(0, 0, 0));
                currentBolts.Add(piece, bolt);
            }

            var line = bolt.GetComponent<LineRenderer>();
            line.SetPositions(new Vector3[] { new Vector3(0, -5), position });

        }

        public void ClearBolts()
        {
            foreach(GameObject bolt in currentBolts.Values)
            {
                bolt.SetActive(false);
            }
            currentBolts.Clear();
        }
    }
}
