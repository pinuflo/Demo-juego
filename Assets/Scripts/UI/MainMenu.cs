using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

    public Text emailInput, errorText, accountText;
    public GameObject LoggedInMenu, LoggedOutMenu;
    public InputField passwordInput;

    public void LogIn()
    {
        string email    = emailInput.text;
        string password = passwordInput.text;
   
        ClientManager.Login(email, password, LogInCallbackSuccess, LogInCallbackError);
    }

    public void Register()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        ClientManager.Register(email, password, RegisterCallbackSuccess, RegisterCallbackError);

    }


    private void RegisterCallbackError(string error)
    {
        errorText.gameObject.SetActive(true);
        errorText.text = error;
    }

    private void RegisterCallbackSuccess()
    {
        string email = emailInput.text;
        string password = passwordInput.text;
        ClientManager.Login(email, password, LogInCallbackSuccess, LogInCallbackError);
    }

    private void LogInCallbackSuccess(PlayerData pData)
    {
        //Se guardan los datos del jugador en el singletón
        PlayerManager.PlayerData = pData;
        accountText.text = pData.email;

        LoggedInMenu.SetActive(true);
        LoggedOutMenu.SetActive(false);
    }


    private void LogInCallbackError(string error)
    {

        LoggedInMenu.SetActive(false);
        LoggedOutMenu.SetActive(true);

        errorText.gameObject.SetActive(true);
        errorText.text = error;
    }

    public void Awake()
    {
        errorText.gameObject.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void LogOut()
    {
        PlayerManager.PlayerData = null;

        LoggedInMenu.SetActive(false);
        LoggedOutMenu.SetActive(true);
    }

    public void Start()
    {
       
        if(PlayerManager.Token == null)
        {
            LoggedInMenu.SetActive(false);
            LoggedOutMenu.SetActive(true);
        }
        else
        {
            LoggedInMenu.SetActive(true);
            LoggedOutMenu.SetActive(false);
            accountText.text = PlayerManager.PlayerData.email;
        }

    }

}
