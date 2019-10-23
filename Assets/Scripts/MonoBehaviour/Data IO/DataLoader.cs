using System;
using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO;
using Assets.Scripts.Workers.IO.Data_Writer;
using Assets.Scripts.Workers.IO.Enemy_Event;
using DataEntities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class DataLoader : MonoBehaviour
    {
        private bool levelDataLoaded;
        private bool userDataLoaded;

        private int width = 0;
        private bool _alreadyLoading = false;

        [SerializeField]
        private Text text;

        private bool LoadingComplete => levelDataLoaded && userDataLoaded;

        public void Start()
        {
            LevelIO.DataLoaded += DataLoaded;
            LevelManager.Instance.LoadData();
            ScoreManager.Instance.Initialise();

            UserDataIO.PiecesCollectedLoadedEvent += PiecesCollectedLoadedEventHandler;
            UserDataIO.LoadUserData();

            StartCoroutine(LoadingText());
        }

        private void PiecesCollectedLoadedEventHandler()
        {
            userDataLoaded = true;
        }

        public void OnDestroy()
        {
            LevelIO.DataLoaded -= DataLoaded;
            UserDataIO.PiecesCollectedLoadedEvent -= PiecesCollectedLoadedEventHandler;
        }

        private void Update()
        {
            if (LoadingComplete && !_alreadyLoading)
            {
                StartCoroutine(LoadSceneAsync());
            }
        }

        public void DataLoaded()
        {
            levelDataLoaded = true;
        }

        IEnumerator LoadSceneAsync()
        {
            _alreadyLoading = true;
    
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scenes.MainMenu);
            asyncLoad.allowSceneActivation = false;

            while (asyncLoad.progress < 0.9f)
            {
                yield return null;
            }

            asyncLoad.allowSceneActivation = true;
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
