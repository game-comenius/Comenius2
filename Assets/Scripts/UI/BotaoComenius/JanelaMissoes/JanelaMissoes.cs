using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanelaMissoes : MonoBehaviour
{
    public bool Ativa { get; set; }
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
    void Start()
    {
        corpoJanelaMissoes = GetComponentInChildren<CorpoJanelaMissoes>();
    }

    public bool CompletamenteExplorada()
    {
        return corpoJanelaMissoes.TodosOsBotoesForamAbertos();
    }
}
