using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.Scripts.Workers.Data_Managers;
using static Assets.Scripts.Constants.Scenes;

public class EndOfGameScreen : MonoBehaviour
{
    [SerializeField]
    private Text Title, Body;

    [SerializeField]
    private Button Retry;

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
        UnityMainThreadDispatcher.Instance().Enqueue(() => SetupButtons());
    }

    public void Show(string body)
    {
        gameObject.SetActive(true);

        this.Title.text = "Failed";
        this.Body.text = body;

        SetupButtons();
    }

    private void SetupButtons()
    {
        Retry.interactable = LivesManager.Instance.LivesRemaining > 0;
    }
        
    public void Retry_Click()
    {
        SceneManager.LoadScene(Game);
    }

    public void Menu_Click()
    {
        SceneManager.LoadScene(MainMenu);
    }
}
