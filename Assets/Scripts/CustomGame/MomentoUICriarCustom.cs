using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentoUICriarCustom : MonoBehaviour {

    [SerializeField]
    private GameObject painelDeProcedimentos;
    [SerializeField]
    private BotaoProcedimentoCriarCustom botaoProcedimentoPrefab;

    private List<BotaoProcedimentoCriarCustom> botoesProcedimento;
    public Procedimento procedimentoSelecionado { get; private set; }

	// Use this for initialization
	void Start () {
        botoesProcedimento = new List<BotaoProcedimentoCriarCustom>();
        foreach (var p in Enum.GetValues(typeof(Procedimento)))
        {
            var botao = Instantiate(botaoProcedimentoPrefab, painelDeProcedimentos.transform);
            botao.Procedimento = (Procedimento) p;
            botoesProcedimento.Add(botao);
            botao.CadastrarMeuMomento(this);
        }
	}

    public void SelecionarProcedimento(BotaoProcedimentoCriarCustom botao)
    {
        procedimentoSelecionado = botao.Procedimento;
        foreach (var botaoProcedimento in botoesProcedimento)
        {
            // Clarear o botão selecionado e escurecer os outros
            if (botaoProcedimento.Equals(botao))
                botaoProcedimento.UpdateColor(true);
            else
                botaoProcedimento.UpdateColor(false);
        }
    }
}
