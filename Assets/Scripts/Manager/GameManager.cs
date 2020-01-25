using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            var clickSobreUI = EventSystem.current.IsPointerOverGameObject();
            // Ou a interface bloqueia o jogo completamente ou ela não bloqueia
            // o jogo mas o último click do jogador foi sobre um objeto da UI
            return _uiSendoUsada || clickSobreUI;
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

            uiSendoUsadaEvent += () => { _uiSendoUsada = true; };

            uiNaoSendoUsadaEvent += () => { _uiSendoUsada = false; };
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        if (_gameManager == this)
        {
            uiSendoUsadaEvent -= () => { _uiSendoUsada = true; };

            uiNaoSendoUsadaEvent -= () => { _uiSendoUsada = false; };
        }
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
