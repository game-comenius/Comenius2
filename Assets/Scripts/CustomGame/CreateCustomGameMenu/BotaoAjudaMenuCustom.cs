using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BotaoAjudaMenuCustom : MonoBehaviour {

	private Button button;

	// Use this for initialization
	private void Start () {
		button = GetComponent<Button>();
        // Botão ajuda começa desativado
		button.interactable = false;
	}


}
