using Assets.Scripts.Constants;
using System.Collections;
using UnityEngine;
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

        private void Start()
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


                if (LevelManager.Instance.LevelUnlocked(i))
                {
                    button.GetComponent<Button>().interactable = true;
                    button.GetComponentInChildren<Text>().text = (i + 1).ToString();
                }
                else
                {
                    button.GetComponent<Button>().interactable = Debug.isDebugBuild;
                    int starsNeeded = LevelManager.Instance.SelectedChapterLevels[i].StarsToUnlock;
                    button.GetComponentInChildren<Text>().text = starsNeeded + " STARS";
                }

                button.GetComponentInChildren<StarPanel>().Configure(LevelManager.Instance.GetLevelProgress(i));
            }
        }

        private void AddEventListener(GameObject button, int i)
        {
            button.GetComponent<Button>().onClick.AddListener(() => LoadLevel(button, i));
        }

        private void LoadLevel(GameObject button, int i)
        {
            LevelManager.Instance.CurrentLevel = i;
            StartCoroutine(LoadSceneAsync(button));
        }

        private IEnumerator LoadSceneAsync(GameObject button)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(Scenes.Game);

            while(!asyncLoad.isDone)
            {
                button.GetComponentInChildren<Text>().text = "PLEASE WAIT";
                yield return null;
            }

        }
    }
}
