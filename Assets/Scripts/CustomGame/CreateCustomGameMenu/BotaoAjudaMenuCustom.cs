using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BotaoAjudaMenuCustom : MonoBehaviour {

	private AjudaMenuCustom ajudaAtual;


    public void CadastrarAjuda(AjudaMenuCustom novaAjuda)
    {
		GetComponent<Button>().interactable = true;

		ajudaAtual = novaAjuda;
    }

    public void DescadastrarAjuda()
    {
        GetComponent<Button>().interactable = false;

        ajudaAtual = null;
    }

    public void AcionarBotao()
    {
		if (ajudaAtual) ajudaAtual.Exibir();
    }
}
