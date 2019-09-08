using Assets.Scripts;
using Assets.Scripts.UI.Helpers;
using GridGeneration;
using UnityEngine;

public delegate void MenuDisplayed();

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

    public void ShowPopup(string title, string body)
    {
        ShowPopup(title, body, null);
    }

    public void ShowPopup(string title, string body, ButtonArgs button)
    {
        ShowPopup(title, body, button, null);
    }

    public void ShowPopup(string title, string body, ButtonArgs button1, ButtonArgs button2)
    {
        GetComponentInChildren<Popup>(true).Show(title, body, button1, button2);
        MenuDisplayed?.Invoke();
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
        MenuDisplayed?.Invoke();
    }

    public void HideMenu<T>()
    {
        ToggleMenuVisiblity<T>(false);
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
