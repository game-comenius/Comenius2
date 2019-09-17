using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _gameManager;

    //public int textOnScene = 0;

    //public bool findOneText = false;

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
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Tem 2 GameManagers");
        }
    }

    //private void OnDrawGizmosSelected()
    //{
    //    textOnScene = FindObjectsOfType<UnityEngine.UI.Text>().Length;

    //    if (findOneText && textOnScene != 0) 
    //    {
    //        GameObject go = FindObjectsOfType<UnityEngine.UI.Text>()[0].gameObject;

    //        string path = go.name;

    //        while (go.transform.root != go.transform)
    //        {
    //            go = go.transform.parent.gameObject;

    //            path = go.name + " -> " + path;
    //        }

    //        findOneText = false;

    //        Debug.Log(path);
    //    }
    //}
}
