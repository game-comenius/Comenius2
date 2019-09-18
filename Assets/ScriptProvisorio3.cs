using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScriptProvisorio3 : MonoBehaviour
{
    public void IrParaSalaDeAula()
    {
        GameManager.uiSendoUsada = false;
        SceneManager.LoadScene(4);
    }

    public void Nao()
    {
        GameManager.uiSendoUsada = false;
        transform.root.gameObject.SetActive(false);
    }
}
