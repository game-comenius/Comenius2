using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptProvisorio2 : MonoBehaviour
{
    public Canvas provisorio;

    private void OnMouseUp()
    {
        provisorio.gameObject.SetActive(true);
        GameManager.uiSendoUsada = true;
    }
}
