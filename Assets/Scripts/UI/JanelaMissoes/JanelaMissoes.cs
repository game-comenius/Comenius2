using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanelaMissoes : MonoBehaviour {

    // Campos relacionados à ajuda do comenius, ou seja, tutoriais
    private static bool fezTutorial;

    public bool Aberta { get; set; }
    private RectTransform transformJanela;
    private Vector2 posicaoFechada;
    private Vector2 posicaoAberta;

    private CorpoJanelaMissoes corpoJanelaMissoes;

    // Esse método deve ser chamado quando você quiser cadastrar uma missão
    // nesta janela de missões, basta passar o título da missão e as
    // ordens/passos/parágrafos da missão
    public void AdicionarMissao(string tituloMissao, string[] ordensMissao)
    {
        corpoJanelaMissoes.AdicionarMissao(tituloMissao, ordensMissao);
    }

	// Use this for initialization
	void Start () {
        transformJanela = transform.GetChild(0).GetChild(0).GetComponent<RectTransform>();
        posicaoFechada = transformJanela.anchoredPosition;
        posicaoAberta = posicaoFechada;
        posicaoAberta.x = 32;

        corpoJanelaMissoes = GetComponentInChildren<CorpoJanelaMissoes>();

        if (fezTutorial) Ativar();
    }

    public void Toggle()
    {
        transformJanela.anchoredPosition = (Aberta) ? posicaoFechada : posicaoAberta;
        Aberta = !Aberta;
    }

    public void Ativar()
    {
        fezTutorial = true;
        GetComponentInChildren<Canvas>().enabled = true;
    }

    public bool CompletamenteExplorada()
    {
        return corpoJanelaMissoes.TodosOsBotoesForamAbertos();
    }
}
