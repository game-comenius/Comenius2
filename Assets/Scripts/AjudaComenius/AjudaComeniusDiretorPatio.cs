using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AjudaComeniusDiretorPatio : MonoBehaviour {

    [SerializeField]
    private NpcDialogo diretor;

    private bool permiteFechar;

    private Canvas canvas;

    // Use this for initialization
    private void Start () {
        canvas = GetComponentInChildren<Canvas>();

        // Cadastrar função para ser invocada quando o diretor fechar o diálogo
        diretor.OnEndDialogueEvent += Mostrar;
	}

    private void Mostrar()
    {
        GameManager.UISendoUsada();
        canvas.enabled = true;

        var coroutine = PermitirFecharApos(8);
        StartCoroutine(coroutine);
    }

    private IEnumerator PermitirFecharApos(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        permiteFechar = true;
    }

    private void Update()
    {
        if (permiteFechar && Input.anyKeyDown)
        {
            canvas.enabled = false;
            GameManager.UINaoSendoUsada();
        }
    }
}
