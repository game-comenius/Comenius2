using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrocaDoDia : MonoBehaviour {

    [SerializeField]
    private NpcDialogo dialogoQueAtivaEstaTroca;
    [SerializeField][Tooltip("Primeira cena do dia seguinte")]
    private string primeiraCenaDiaSeguinte;

	// Use this for initialization
	private void Start () {
        dialogoQueAtivaEstaTroca.OnEndDialogueEvent += Trocar;
	}

    private void Trocar()
    {
        StartCoroutine(TrocarCoroutine());
    }

    private IEnumerator TrocarCoroutine()
    {
        var preparador = GetComponent<PreparadorDaProximaMissao>();
        if (preparador) preparador.LimparMissaoAtual();

        // Escurecer a tela se houver um FadeEffect como filho deste objeto
        var fade = GetComponentInChildren<FadeEffect>();
        if (fade) yield return StartCoroutine(fade.Fade(1));
        yield return new WaitForSeconds(0.5f);

        var sceneLoader = GetComponent<SceneLoader>();
        if (sceneLoader)
        {
            sceneLoader.LoadNewScene(primeiraCenaDiaSeguinte);
        }
        else
        {
            // Se não tem SceneLoader, tentar com GoToScene
            var goToScene = GetComponent<GoToScene>();
            if (goToScene) goToScene.IrParaCena();
        }
    }
}
