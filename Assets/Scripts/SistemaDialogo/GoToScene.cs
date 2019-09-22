using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToScene : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 4;

    public void IrParaCena()
    {
        GameManager.uiSendoUsada = true;
        SceneManager.LoadScene(sceneIndex);
    }
}
