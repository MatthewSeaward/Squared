using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using Assets.Scripts.UI.Helpers;
using Assets.Scripts.Constants;

public class Popup : MonoBehaviour
{
    [SerializeField]
    private Text Title, Body;

    [SerializeField]
    private Button Button1;

    [SerializeField]
    private Button Button2;

 
    public void Show(string title, string body)
    {
        Show(title, body, null, null);
    }

    public void Show(string title, string body, ButtonArgs button1)
    {
        Show(title, body, button1, null);
    }

    public void Show(string title, string body, ButtonArgs button1, ButtonArgs button2)
    {
        gameObject.SetActive(true);

        this.Title.text = title;
        this.Body.text = body;

        SetupButton(this.Button1, button1);
        SetupButton(this.Button2, button2);
    }

    private void SetupButton(Button button, ButtonArgs buttonArgs)
    {
        if (buttonArgs == null || string.IsNullOrEmpty(buttonArgs.Text))
        {
            button.gameObject.SetActive(false);
        }
        else
        {
            button.interactable = buttonArgs.Enabled;
            button.gameObject.SetActive(true);
            button.GetComponentInChildren<Text>().text = buttonArgs.Text;
            button.onClick.AddListener(buttonArgs.Action);
        }
    }
        
    public void Menu_Click()
    {
        SceneManager.LoadScene(Scenes.MainMenu);
    }

}
