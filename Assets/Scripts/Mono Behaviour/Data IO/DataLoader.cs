using System;
using System.Collections;
using System.Threading.Tasks;
using Assets.Scripts.Constants;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Data_Managers;
using Assets.Scripts.Workers.IO;
using Assets.Scripts.Workers.IO.Data_Writer;
using Assets.Scripts.Workers.IO.Enemy_Event;
using Assets.Scripts.Workers.IO.Level_Loader.Order;
using LevelLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class DataLoader : MonoBehaviour
    {
        private const float LoadConfigTimeout = 5f;
        private int width = 0;
        private bool _alreadyLoading = false;

        [SerializeField]
        private Text text;

        [SerializeField]
        private Text loadingDetails;

        public async void Start()
        {
            LevelIO.Instance.Initialise(new FileLevelLoader(), new FireBaseLevelProgressReader(), new FireBaseLevelProgressWriter(), new LevelOrderLoader(), new JSONEventReader());
            
            StartCoroutine(LoadData());

            await LoadDataAsync(() => LoadLevelProgress(), "Loading Level Progress");

            StartCoroutine(LoadingText());
        }

        private async Task LoadLevelProgress()
        {
            await LevelIO.Instance.LoadLevelProgress();
        }

        private IEnumerator LoadData()
        {
            var levelIO = LevelIO.Instance;
            var userIO = UserIO.Instance;

            yield return LoadData(() => levelIO.LoadLevels(), "Loading Levels");
            yield return LoadData(() => levelIO.LoadLevelStars(), "Loading Level Stars");
            yield return LoadData(() => levelIO.LoadEnemyEvents(), "Loading Level Events");
            yield return LoadData(() => LoadConfig(), "Loading Config");

            yield return LoadData(() => userIO.LoadPowerupsData(), "Loading Powerups");
            yield return LoadData(() => userIO.LoadEquippedPowerups(), "Loading Equipped Powerups");
            yield return LoadData(() => userIO.LoadLives(), "Loading Lives");

            yield return LoadData(() => PieceCollectionIO.Instance.LoadCollectionData(), "Loading Collected Pieces");

            ScoreManager.Instance.Initialise();
            PieceCollectionManager.Instance.Initialise();

            if (!_alreadyLoading)
            {
                StartCoroutine(LoadSceneAsync());
            }
        }

        private async Task LoadDataAsync(Func<Task> task, string status)
        {
            ReportStatus(status);
            await task();
        }

        private IEnumerator LoadData(Action action, string status)
        {
            ReportStatus(status);
            yield return null;

            action();
            yield return null;
        }
     
        private void LoadConfig()
        {
            var startTime = DateTime.Now;

            var task = Firebase.RemoteConfig.FirebaseRemoteConfig.FetchAsync();

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

            Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched();
        }

        private void ReportStatus(string status)
        {
            Debug.Log(status);
            loadingDetails.text = status;
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