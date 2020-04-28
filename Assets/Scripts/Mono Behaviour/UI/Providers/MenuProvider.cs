using Assets.Scripts;
using Assets.Scripts.Managers;
using GridGeneration;
using UnityEngine;

public delegate void MenuDisplayed(System.Type type);

public class MenuProvider : MonoBehaviour
{
    public static MenuDisplayed MenuDisplayed;

    public bool OnDisplay
    {
        get
        {
            foreach (Transform panel in transform)
            {
                if (panel.gameObject.activeInHierarchy)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public static MenuProvider Instance { private set; get; }

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        HideAll();
        BasicGridGenerator.GridGenerated += ShowLevelStart;
    }
 
    public void ShowEndOfGameScreen(string body)
    {
        GetComponentInChildren<EndOfGameScreen>(true).Show(body);
        MenuDisplayed?.Invoke(typeof(EndOfGameScreen));
    }

    public void ShowVictoryPopup(int score, int target)
    {
        GetComponentInChildren<VictoryScreen>(true).Show(score, target);
        MenuDisplayed?.Invoke(typeof(VictoryScreen));
    }

    public void ShowLevelStart()
    {
        BasicGridGenerator.GridGenerated -= ShowLevelStart;

        ShowMenu<LevelStart>();
    }

    public void HideAll()
    {
        foreach(Transform panel in transform)
        {
            panel.gameObject.SetActive(false);
        }

        GameManager.Instance.ChangePauseState(this, OnDisplay);
    }

    public void PauseClicked()
    {
        ShowMenu<InGameMenu>();
    }

    public void ShowMenu<T>()
    {
        if (OnDisplay)
        {
            return;
        }

        ToggleMenuVisiblity<T>(true);
        MenuDisplayed?.Invoke(typeof(T));

        GameManager.Instance.ChangePauseState(this, OnDisplay);
    }

    public void HideMenu<T>()
    {
        ToggleMenuVisiblity<T>(false);

        GameManager.Instance.ChangePauseState(this, OnDisplay);
    }

    private void ToggleMenuVisiblity<T>(bool show)
    {
        var item = GetComponentInChildren<T>(true) as MonoBehaviour;
        if (item == null)
        {
            return;
        }

        item.gameObject.SetActive(show);
    }
}
