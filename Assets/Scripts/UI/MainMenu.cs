using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour {

    public Text emailInput, passwordInput, errorText, accountText;
    public GameObject LoggedInMenu, LoggedOutMenu;

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

    public void postScore(int points)
    {
        ClientManager.SaveScore(points, PlayerManager.Token, postScoreSuccess, postScoreFail);
    }

    private void postScoreFail(string error)
    {
        Debug.Log(error);
    }

    private void postScoreSuccess()
    {

    }

    private void RegisterCallbackError(string error)
    {
        errorText.gameObject.SetActive(true);
        errorText.text = error;
    }

    private void RegisterCallbackSuccess()
    {
        Debug.Log("Cuenta creada con éxito");
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
