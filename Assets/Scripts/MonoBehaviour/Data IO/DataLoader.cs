using System.Collections;
using Assets.Scripts.Constants;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.IO;
using Assets.Scripts.Workers.IO.Data_Writer;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class DataLoader : MonoBehaviour
    {
        private bool levelDataLoaded;
        private bool userDataLoaded;
        private bool configLoaded;

        private int width = 0;
        private bool _alreadyLoading = false;

        [SerializeField]
        private Text text;

        private bool LoadingComplete => levelDataLoaded && userDataLoaded && configLoaded;

        public void Start()
        {
            LevelIO.DataLoaded += Level_DataLoaded;
            UserIO.DataLoaded += User_DataLoaded;

            LevelManager.Instance.LoadData();
            UserIO.Instance.LoadData();

            ScoreManager.Instance.Initialise();
         
            StartCoroutine(LoadConfig());

            StartCoroutine(LoadingText());
        }

        IEnumerator LoadConfig()
        {
            var task = Firebase.RemoteConfig.FirebaseRemoteConfig.FetchAsync();
            while (!task.IsCompleted)
            {
                yield return null;
            }

            Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched();
            configLoaded = true;
        }
        
        public void OnDestroy()
        {
            LevelIO.DataLoaded -= Level_DataLoaded;
            UserIO.DataLoaded -= User_DataLoaded;
        }

        private void Update()
        {
            if (LoadingComplete && !_alreadyLoading)
            {
                StartCoroutine(LoadSceneAsync());
            }
        }

        public void Level_DataLoaded()
        {
            levelDataLoaded = true;
        }


        public void User_DataLoaded()
        {
            userDataLoaded = true;
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
