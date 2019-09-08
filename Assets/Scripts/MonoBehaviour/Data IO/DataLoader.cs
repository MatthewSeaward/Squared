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
        private int width = 0;

        [SerializeField]
        private Text text;

        public void Start()
        {
            LevelIO.DataLoaded += DataLoaded;
            LevelManager.Instance.LoadData();
            ScoreManager.Instance.Initialise();

            StartCoroutine(LoadingText());
        }
     
        public void OnDestroy()
        {
            LevelIO.DataLoaded -= DataLoaded;
        }

        private void Update()
        {
            if (_loaded)
            {
                StartCoroutine(LoadSceneAsync());
            }
        }

        public void DataLoaded()
        {
            _loaded = true;
        }

        IEnumerator LoadSceneAsync()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scenes.MainMenu);

            while(!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        IEnumerator LoadingText()
        {
            while(true)
            {
                text.text = "LOADING" +  "".PadRight(width++, '.');
                if (width > 3)
                {
                    width = 0;
                }
                yield return new WaitForSeconds(GameSettings.LoadingTextUpdateTime);
            }
        }

    }
}
