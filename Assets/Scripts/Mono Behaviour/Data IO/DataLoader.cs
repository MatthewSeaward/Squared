using System;
using System.Collections;
using System.Threading.Tasks;
using Assets.Scripts.Constants;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Managers;
using Assets.Scripts.Workers.IO;
using Assets.Scripts.Workers.IO.Level_Loader.Order;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.Workers.IO.Scripts.Workers.IO.Data_Entities;
using Assets.Scripts.Workers.IO.Score;

namespace Assets.Scripts
{
    class DataLoader : MonoBehaviour
    {
        private const float LoadConfigTimeout = 5f;
        private const float LoadingTimeout = 20f;

        private float _loadingtime = 0;
        private int width = 0;
        private bool _alreadyLoading = false;

        [SerializeField]
        private Text text;

        [SerializeField]
        private Text loadingDetails;

        [SerializeField]
        private Text checkInternet;

        public async void Start()
        {
            checkInternet.text = string.Empty;

            StartCoroutine(LoadingText());

            LevelIO.Instance.Initialise(new FileLevelLoader(), new FireBaseLevelProgressReader(), new FireBaseLevelProgressWriter(), new LevelOrderLoader(), new JSONEventReader());
            
            await LoadData();     
        }

        private async Task LoadData()
        {
            var levelIO = LevelIO.Instance;
            var userIO = UserIO.Instance;

            LoadData(() => levelIO.LoadLevels(), "Loading Levels");

            await LoadDataAsync(() => levelIO.LoadLevelProgress(), "Loading Level Progress");
            await LoadDataAsync(() => LoadConfig(), "Loading Config");
            await LoadDataAsync(() => userIO.LoadPowerupsData(), "Loading Powerups");
            await LoadDataAsync(() => userIO.LoadLives(), "Loading Lives");
            await LoadDataAsync(() => PieceCollectionIO.Instance.LoadCollectionData(), "Loading Collected Pieces");

            ScoreManager.Instance.Initialise();
            PieceCollectionManager.Instance.Initialise();
           
             StartCoroutine(LoadSceneAsync());
        }

        private async Task LoadDataAsync(Func<Task> task, string status)
        {
            ReportStatus(status);
            await task();
        }

        private void LoadData(Action task, string status)
        {
            ReportStatus(status);
            task();
        }
     
        private async Task LoadConfig()
        {
            var startTime = DateTime.Now;

            await Firebase.RemoteConfig.FirebaseRemoteConfig.FetchAsync().ContinueWith((task) =>
            {
                while (!task.IsCompleted)
                {
                    if (task.IsCanceled || task.IsFaulted)
                    {
                        break;
                    }

                    var timePast = DateTime.Now - startTime;
                    if (timePast.TotalSeconds >= LoadConfigTimeout)
                    {
                        break;
                    }
                }
            });

            Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched();
        }

        private void ReportStatus(string status)
        {
            Debug.Log(status);
            loadingDetails.text = status;
        }

        private void Update()
        {
            _loadingtime += Time.deltaTime;

            if (_loadingtime > LoadingTimeout)
            {
                _loadingtime = 0f;
                checkInternet.text = "Taking too long? Check your internet connection";
            }
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