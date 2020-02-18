﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanelaMissoes : MonoBehaviour {

    // Define se a janela deve ser renderizada ou não
    private static bool ativada;

    public bool Aberta { get; set; }

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
        // Esta janela de missões estará presente em todas as cenas seguintes
        // Não é necessário adicionar o GameObject/Prefab em outras cenas
        DontDestroyOnLoad(this.gameObject);

        corpoJanelaMissoes = GetComponentInChildren<CorpoJanelaMissoes>();

        if (ativada)
            Ativar();
        else
            Desativar();

    }

    public void Toggle()
    {
        Aberta = !Aberta;
        GetComponent<Animator>().SetBool("Aberta", Aberta);
    }

    public bool Abrir()
    {
        if (Aberta) return false;
        Toggle();
        return true;
    }

    public bool Fechar()
    {
        if (!Aberta) return false;
        Toggle();
        return true;
    }

    public void Ativar()
    {
        ativada = true;
        GetComponentInChildren<Canvas>().enabled = true;
    }

    public void Desativar()
    {
        ativada = false;
        GetComponentInChildren<Canvas>().enabled = false;
    }

    public bool CompletamenteExplorada()
    {
        return corpoJanelaMissoes.TodosOsBotoesForamAbertos();
    }
}
