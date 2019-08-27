using System;
using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.Workers.IO;
using Assets.Scripts.Workers.IO.Data_Writer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class DataLoader : MonoBehaviour
    {
        private bool _loaded = false;

        public void Start()
        {
            LevelIO.DataLoaded += DataLoaded;
            LevelManager.Instance.LoadData();
            ScoreManager.Instance.Initialise();
        }
     
        public void OnDestroy()
        {
            LevelIO.DataLoaded -= DataLoaded;
        }

        private void Update()
        {
            if (_loaded)
            {
                SceneManager.LoadScene(Scenes.MainMenu);
            }
        }

        public void DataLoaded()
        {
            _loaded = true;
        }

    }
}
