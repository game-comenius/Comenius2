using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddUsoUI : MonoBehaviour 
{
    [Tooltip ("Se for false ativa UINaoSendoUsada")]
    [SerializeField] private bool botaoAtivaUISendoUsada = true;

    private void Start()
    {
        if (botaoAtivaUISendoUsada)
        {
            GetComponent<Button>().onClick.AddListener(() => GameManager.UISendoUsada());
        }
        else
        {
            GetComponent<Button>().onClick.AddListener(() => GameManager.UINaoSendoUsada());
        }
    }
}
