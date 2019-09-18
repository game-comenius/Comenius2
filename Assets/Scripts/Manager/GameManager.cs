using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManager;

    public static GameManager gameManager
    {
        get
        {
            return _gameManager;
        }
    }

    private static bool _uiSendoUsada = false;

    public static bool uiSendoUsada
    {
        get
        {
            return _uiSendoUsada;
        }

        set
        {
            _uiSendoUsada = value;
        }
    }

    private void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Tem 2 GameManagers");
        }
    }
}
