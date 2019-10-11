using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fichario : MonoBehaviour {

    [SerializeField]
    private GameObject folhaInventario;
    [SerializeField]
    private GameObject folhaMapa;
    [SerializeField]
    private GameObject folhaDiario;

    private static GameObject folhaSelecionada;

    // Use this for initialization
    void Start () {
        // gameObject.SetActive(false);

        if (!folhaSelecionada) folhaSelecionada = folhaInventario;
        SelecionarInventario();

	}

    private void OnEnable()
    {
        if (!GameManager.uiSendoUsada) GameManager.UISendoUsada();
    }
    private void OnDisable()
    {
        if (GameManager.uiSendoUsada) GameManager.UINaoSendoUsada();
    }

    public void SelecionarDiario()
    {
        if (folhaSelecionada == folhaDiario) return;

        folhaSelecionada = folhaDiario;
        folhaInventario.transform.SetAsFirstSibling();
        folhaMapa.transform.SetAsFirstSibling();
    }

    public void SelecionarMapa()
	{
        if (folhaSelecionada == folhaMapa) return;

        folhaSelecionada = folhaMapa;
        folhaInventario.transform.SetAsFirstSibling();
        folhaDiario.transform.SetAsFirstSibling();
    }

    public void SelecionarInventario()
	{
        if (folhaSelecionada == folhaInventario) return;

        folhaSelecionada = folhaInventario;
        folhaMapa.transform.SetAsFirstSibling();
        folhaDiario.transform.SetAsFirstSibling();
	}
}
