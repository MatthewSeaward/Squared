using Assets.Scripts.Constants;
using Assets.Scripts.Workers.Helpers;
using Assets.Scripts.Workers.Managers;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class MenuLevelSelection :MonoBehaviour
    {
        [SerializeField]
        private GameObject Button;

        [SerializeField]
        private Transform ScrollContent;

        private bool _alreadyLoading = false;

        private void Awake()
        {
            LivesManager.LivesChanged += LivesManager_LivesChanged;
        }

        private void OnDestroy()
        {
            LivesManager.LivesChanged -= LivesManager_LivesChanged;
        }

        private void LivesManager_LivesChanged(bool gained, int newLives)
        {
            try
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() => Refresh());                
            }
            catch (Exception ex)
            {
                Workers.Helpers.DebugLogger.Instance.WriteException(ex);
            }
        }

        private void OnEnable()
        {
            Refresh();
        }      

        public void Refresh()
        {
            foreach(Transform item in ScrollContent)
            {
                item.gameObject.SetActive(false);
            }

            var levels = LevelManager.Instance.SelectedChapterLevels;

            for (int i = 0; i < levels.Length; i++)
            {
                var button = ObjectPool.Instantiate(Button, Vector3.zero);

                AddEventListener(button, i);
                button.GetComponentInChildren<Text>().text = (i + 1).ToString();
                button.transform.SetParent(ScrollContent);
                button.transform.localScale = new Vector3(1, 1, 1);


                if (LevelManager.Instance.CanPlayLevel(i))
                {
                    button.GetComponent<Button>().interactable = true;
                    button.GetComponentInChildren<Text>().text = (i + 1).ToString();
                }
                else
                {
                    button.GetComponent<Button>().interactable = Debug.isDebugBuild;
                    int starsNeeded = LevelManager.Instance.SelectedChapterLevels[i].StarsToUnlock;

                    button.GetComponentInChildren<Text>().text = LivesManager.Instance.LivesRemaining == 0 ? "NO CREDITS":  starsNeeded + " STARS";
                }

                button.GetComponentInChildren<StarPanel>().Configure(LevelManager.Instance.GetStarAchievedOnLevel(i));
            }
        }

        private void AddEventListener(GameObject button, int i)
        {
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(button, i));
        }

        private void LoadLevel(GameObject button, int i)
        {
            if (_alreadyLoading)
            {
                return;
            }
            LevelManager.Instance.DailyChallenge = false;
            LevelManager.Instance.CurrentLevel = i;

            StartCoroutine(LoadSceneAsync(button));
        }

        private IEnumerator LoadSceneAsync(GameObject button)
        {
            _alreadyLoading = true;

            FindObjectOfType<EventSystem>().gameObject.SetActive(false);

            button.GetComponentInChildren<Text>().text = "PLEASE WAIT";

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scenes.Game);

            asyncLoad.allowSceneActivation = false;

            while (asyncLoad.progress < 0.9f)
            {
                yield return null;
            }

            asyncLoad.allowSceneActivation = true;
        }
    }
}
