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
    }


    public delegate void UsoUI();

    public static event UsoUI uiSendoUsadaEvent;

    public static event UsoUI uiNaoSendoUsadaEvent;

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

        uiSendoUsadaEvent += () => { _uiSendoUsada = true; };

        uiNaoSendoUsadaEvent += () => { _uiSendoUsada = false; };
    }

    private void OnDestroy()
    {
        uiSendoUsadaEvent -= () => { _uiSendoUsada = true; };

        uiNaoSendoUsadaEvent -= () => { _uiSendoUsada = false; };
    }

    public static void UISendoUsada()
    {
        uiSendoUsadaEvent();
    }

    public static void UINaoSendoUsada()
    {
        uiNaoSendoUsadaEvent();
    }

    public void UISendoUsadaParaBotao()
    {
        uiSendoUsadaEvent();
    }

    public void UINaoSendoUsadaParaBotao()
    {
        uiNaoSendoUsadaEvent();
    }
}
