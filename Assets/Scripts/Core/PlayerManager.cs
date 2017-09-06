using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    protected PlayerData _playerData = null;
    public static PlayerData PlayerData
    {
        set
        {
            Instance._playerData = value;
        }
        get
        {
            if(Instance != null && Instance._playerData != null)
                return Instance._playerData;
            return null;
        }
    }

    private static PlayerManager _instance = null;
    public static PlayerManager Instance
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

    public int _customSpeed    = 0;
    public int _customDuration = 0;
    public int _customPointsPerCoin = 0;


    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);
    }

    public static string Token
    {
        get
        {
            if(PlayerData != null)
                return PlayerData.api_token;
            return null;
        }
    }

    //En caso de que se escoja un valor personalizado se ignora el obtenido por la API
    public static int Speed
    {
        get
        {
            if(Instance != null)
            {
                if (Instance._customSpeed != 0)
                {
                    return Instance._customSpeed;
                }
                else
                {
                    if (PlayerData != null)
                        return PlayerData.velocidad;
                    return 1;
                }
            }
            return 1;

        }
    }

    //En caso de que se escoja un valor personalizado se ignora el obtenido por la API
    public static int Duration
    {
        get
        {
            if(Instance != null)
            {
                if (Instance._customDuration != 0)
                {
                    return Instance._customDuration;
                }
                else
                {
                    if (PlayerData != null)
                        return PlayerData.tiempo_duracion;
                    return 30;
                }
            }

            return 30;



        }
    }

    //En caso de que se escoja un valor personalizado se ignora el obtenido por la API
    public static int PointsPerItem
    {
        get
        {
            if (PlayerData != null)
                return PlayerData.puntos_por_item;
            return 10;
        }
    }


}
