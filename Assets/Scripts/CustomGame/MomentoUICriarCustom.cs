using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MomentoUICriarCustom : MonoBehaviour {

    private GameObject painelDeProcedimentos;
    private GameObject painelDeAgrupamentos;

    [SerializeField]
    private BotaoProcedimentoCriarCustom botaoProcedimentoPrefab;
    [SerializeField]
    private BotaoAgrupamentoCriarCustom botaoAgrupamentoPrefab;

    private List<BotaoProcedimentoCriarCustom> botoesProcedimento;
    public Procedimento ProcedimentoSelecionado { get; private set; }

    private List<BotaoAgrupamentoCriarCustom> botoesAgrupamento;
    public Agrupamento AgrupamentoSelecionado { get; private set; }

	// Use this for initialization
    	void Start () {
        var grids = GetComponentsInChildren<GridLayoutGroup>();
        painelDeProcedimentos = grids[0].gameObject;
        painelDeAgrupamentos = grids[1].gameObject;

        botoesProcedimento = new List<BotaoProcedimentoCriarCustom>();
        foreach (var p in Enum.GetValues(typeof(Procedimento)))
        {
            var botao = Instantiate(botaoProcedimentoPrefab, painelDeProcedimentos.transform);
            botao.Procedimento = (Procedimento) p;
            botoesProcedimento.Add(botao);
            botao.CadastrarMeuMomento(this);
        }

        botoesAgrupamento = new List<BotaoAgrupamentoCriarCustom>();
        foreach (var a in Enum.GetValues(typeof(Agrupamento)))
        {
            var botao = Instantiate(botaoAgrupamentoPrefab, painelDeAgrupamentos.transform);
            botao.Agrupamento = (Agrupamento)a;
            botoesAgrupamento.Add(botao);
            botao.CadastrarMeuMomento(this);
        }
    }

    public void SelecionarProcedimento(BotaoProcedimentoCriarCustom botao)
    {
        ProcedimentoSelecionado = botao.Procedimento;
        foreach (var botaoProcedimento in botoesProcedimento)
        {
            // Clarear o botão selecionado e escurecer os outros
            if (botaoProcedimento.Equals(botao))
                botaoProcedimento.UpdateColor(true);
            else
                botaoProcedimento.UpdateColor(false);
        }
    }

    public void SelecionarAgrupamento(BotaoAgrupamentoCriarCustom botao)
    {
        AgrupamentoSelecionado = botao.Agrupamento;
        foreach (var botaoAgrupamento in botoesAgrupamento)
        {
            // Clarear o botão selecionado e escurecer os outros
            if (botaoAgrupamento.Equals(botao))
                botaoAgrupamento.UpdateColor(true);
            else
                botaoAgrupamento.UpdateColor(false);
        }
    }
}
