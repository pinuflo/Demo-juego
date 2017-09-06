using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClientManager : MonoBehaviour 
{

	public static string base_url = "http://demo.telectric.cl/Demo-Juego-Web/public/api/";

	private static ClientManager _instance = null;
	public static ClientManager Instance
	{
		get
		{
			return _instance;
		}
		set
		{
			_instance = value;
		}
	}

   
		
	private void Awake()
	{
        DontDestroyOnLoad(transform.gameObject);
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
    }

    /**
     * 
     *  Metódo para iniciar sesión en el servidor y almacenar los datos devueltos por el servidor. Es de uso interno, acceder a Login en su lugar.
     * 
     **/
    public IEnumerator LoginIenumerator(string email, string password, LoginSuccess callbackSuccess, LoginError callbackError)
	{
		string url = base_url + "login";

   
        WWWForm form = new WWWForm ();
		form.AddField ("email", email);
		form.AddField ("password", password);

		var headers = form.headers;
		headers["Accept"] 		= "application/json";

		WWW www = new WWW(url,form.data, headers);
		yield return www;
  
        JSONObject json = new JSONObject(www.text);

        bool success =  false;
        if (json.HasField("success") == true)
            success = (json.GetField("success").ToString() == "true") ? true : false ;

        if (success == true)
        {
            string data = json.GetField("data").ToString();
            PlayerData pData = JsonUtility.FromJson<PlayerData>(data);
            Debug.Log(data);
            callbackSuccess(pData);
        }
        else
        {
            callbackError("Error de autentificación");
        }

	}


    /**
     * 
     *  Metódo para registrar un nuevo usuario, Es de uso interno, acceder a Register en su lugar.
     * 
     **/
    public IEnumerator RegisterIenumerator(string email, string password, RegisterSuccess callbackSuccess, RegisterError callbackError)
    {
        string url = base_url + "register";


        WWWForm form = new WWWForm();
        form.AddField("email", email);
        form.AddField("password", password);
        form.AddField("password_confirmation", password);

        var headers = form.headers;
        headers["Accept"] = "application/json";

        WWW www = new WWW(url, form.data, headers);
        yield return www;

        JSONObject json = new JSONObject(www.text);

        bool success = false;
        if (json.HasField("success") == true)
            success = (json.GetField("success").ToString() == "true") ? true : false;

        if (success == true)
        {
            string data = json.GetField("data").ToString();
            PlayerData pData = JsonUtility.FromJson<PlayerData>(data);
            callbackSuccess();
        }
        else
        {
            Debug.Log(www.text);
            callbackError("Error al crear nueva cuenta");
        }

    }

    /**
     * 
     *  Metódo para registrar un nuevo puntaje a traves del uso de un Token de identifación. Es de uso interno, acceder a SaveScore en su lugar.
     * 
     **/
    public IEnumerator SaveScoreIenumerator(int points, string token, SaveScoreSuccess callbackSuccess, SaveScoreError callbackError)
    {
        string url = base_url + "score";
         

        WWWForm form = new WWWForm();
        form.AddField("points", points);

        var headers = form.headers;
        headers["Accept"]        = "application/json";
        headers["Authorization"] = "Bearer " + token;


        WWW www = new WWW(url, form.data, headers);
        yield return www;

        JSONObject json = new JSONObject(www.text);

        bool success = false;
        if (json.HasField("success") == true)
            success = (json.GetField("success").ToString() == "true") ? true : false;

        if (success == true)
        {
            if(callbackSuccess != null)
                callbackSuccess();
        }
        else
        {
            string error = json.GetField("message").ToString();
            if (callbackError != null)
                callbackError(error);
        }


    }


    /**
     * 
     *  Metódo para iniciar sesión en el servidor y almacenar los datos devueltos por el servidor.
     * 
     **/
    public static void Login(string email, string password, LoginSuccess callbackSuccess, LoginError callbackError)
    {
        if (Instance != null)
            Instance.StartCoroutine(Instance.LoginIenumerator(email, password, callbackSuccess, callbackError));
    }

    /**
     * 
     *  Metódo para registrar un nuevo usuario, Es de uso interno, acceder a Register en su lugar.
     * 
     **/
    public static void Register(string email, string password, RegisterSuccess callbackSuccess, RegisterError callbackError)
    {
        if (Instance != null)
            Instance.StartCoroutine(Instance.RegisterIenumerator(email, password, callbackSuccess, callbackError));
    }

    /**
     * 
     *  Metódo para registrar un nuevo puntaje a traves del uso de un Token de identifación.
     * 
     **/
    public static void SaveScore( int points, string token, SaveScoreSuccess callbackSuccess=null, SaveScoreError callbackError=null)
    {
        if(Instance != null)
            Instance.StartCoroutine(Instance.SaveScoreIenumerator(points, token, callbackSuccess, callbackError));
    }


    public delegate void LoginSuccess(PlayerData pData);
    public delegate void LoginError(string error);

    public delegate void RegisterSuccess();
    public delegate void RegisterError(string error);

    public delegate void SaveScoreSuccess();
    public delegate void SaveScoreError(string error);

} 
