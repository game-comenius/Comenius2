﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Linq;
using System.Globalization;

public class CreateCustomGamePanel : MonoBehaviour
{
    // struct útil para transferir as informações criadas para o disco
    // e depois resgatá-las para a memória durante o load de um jogo criado
    [Serializable()]
    public struct MidiaPoderFeedback
    {
        public ItemName Midia;
        public Poder Poder;
        public string Feedback;
    }

    // Campos para não ter que procurar depois
    public PaginaIntroducaoCustom paginaIntroducaoCustom;
    public PaginaEscolherProfessor paginaEscolherProfessor;
    public PaginaInformacoesBasicas paginaInformacoesBasicas;
    public PaginaProcedimentoAgrupamento paginaProcedimentoAgrupamento;
    public PaginaEscolherMidias paginaEscolherMidias;
    public PaginaFeedbackMidias paginaFeedbackMidiasMomento1;
    public PaginaFeedbackMidias paginaFeedbackMidiasMomento2;
    public PaginaFeedbackMidias paginaFeedbackMidiasMomento3;
    public PaginaResumoSalvar paginaResumoSalvar;

    // Colocar as páginas acima em uma linked list para caminhar por elas
    private LinkedList<GameObject> listaDePaginas;
    private LinkedListNode<GameObject> nodoPaginaAtual;

    public Button botaoVoltarPagina;
    public Button botaoAvancarPagina;

    [SerializeField]
    private GameObject listaFalasProfessor;
    private TMP_InputField introducaoAula;
    private TMP_InputField descricaoMomento1;
    private TMP_InputField descricaoMomento2;
    private TMP_InputField descricaoMomento3;

    [SerializeField]
    private TMP_InputField tituloDaAula;
    [SerializeField]
    private TMP_InputField autorInputField;

    [SerializeField]
    private BotaoAjudaMenuCustom BotaoAjudaMenuCustom;

    private void Awake()
    {
        // Colocar as páginas em uma lista para caminhar por elas depois
        listaDePaginas = new LinkedList<GameObject>();
        listaDePaginas.AddLast(paginaIntroducaoCustom.gameObject);
        listaDePaginas.AddLast(paginaEscolherProfessor.gameObject);
        listaDePaginas.AddLast(paginaInformacoesBasicas.gameObject);
        listaDePaginas.AddLast(paginaProcedimentoAgrupamento.gameObject);
        listaDePaginas.AddLast(paginaEscolherMidias.gameObject);
        listaDePaginas.AddLast(paginaFeedbackMidiasMomento1.gameObject);
        listaDePaginas.AddLast(paginaFeedbackMidiasMomento2.gameObject);
        listaDePaginas.AddLast(paginaFeedbackMidiasMomento3.gameObject);
        listaDePaginas.AddLast(paginaResumoSalvar.gameObject);

        // Ativar as páginas para buscar os scripts necessários
        foreach (var pagina in listaDePaginas) pagina.SetActive(true);

        //// Coletar falas do professor na sala dos professores
        var falas = listaFalasProfessor.GetComponentsInChildren<TMP_InputField>();
        introducaoAula = falas[0];
        descricaoMomento1 = falas[1];
        descricaoMomento2 = falas[2];
        descricaoMomento3 = falas[3];

        // Desativar todas as páginas deste panel e ativar apenas a primeira
        foreach (var pagina in listaDePaginas) pagina.SetActive(false);
        IrParaPagina(listaDePaginas.First);
    }

    public void IrParaPagina(LinkedListNode<GameObject> nodoDaPaginaAlvo)
    {
        // Desativar página que será trocada pela página alvo
        if (nodoPaginaAtual != null) nodoPaginaAtual.Value.SetActive(false);

        // Ativar página alvo
        nodoPaginaAtual = nodoDaPaginaAlvo;
        nodoPaginaAtual.Value.SetActive(true);

        // Se a página alvo é a primeira página, desabilitar botão voltar
        bool primeiraPagina = (nodoPaginaAtual == listaDePaginas.First);
        if (primeiraPagina)
            botaoVoltarPagina.gameObject.SetActive(false);
        else
            botaoVoltarPagina.gameObject.SetActive(true);

        // Se a página alvo é a última página, desabilitar botão avançar
        bool ultimaPagina = (nodoPaginaAtual == listaDePaginas.Last);
        if (ultimaPagina)
            botaoAvancarPagina.gameObject.SetActive(false);
        else
            botaoAvancarPagina.gameObject.SetActive(true);

        // Se a página possuir uma ajuda, exibir esta ajuda se ela ainda não
        // foi exibida. Também cadastrar esta ajuda no botão de ajuda da página
        // para que ela possa ser acessada sempre que o jogador quiser
        var ajuda = GetComponentInChildren<AjudaMenuCustom>();
        if (ajuda)
        {
            if (!ajuda.JaFoiExibida) ajuda.Exibir();
            BotaoAjudaMenuCustom.CadastrarAjuda(ajuda);
        }
        else
        {
            BotaoAjudaMenuCustom.DescadastrarAjuda();
        }
    }

    // Métodos vinculados a botões
    public void IrParaProximaPagina()
    {
        if (nodoPaginaAtual.Next != null) IrParaPagina(nodoPaginaAtual.Next);
    }

    public void IrParaPaginaAnterior()
    {
        if (nodoPaginaAtual.Previous != null) IrParaPagina(nodoPaginaAtual.Previous);
    }

    public void PressButtonThatStartsCustomGame()
    {
        // Criar configurações a partir das escolhas do jogador
        CustomGameSettings settings = CriarCustomGameSettings();

        // Definir essa configuração como a configuração atual
        CustomGameSettings.CurrentSettings = settings;
    }

    public void PressButtonThatSavesCustomGame()
    {
        // Criar configurações a partir das escolhas do jogador e
        // criar objeto para escrever no disco
        CustomGameSettings settings = CriarCustomGameSettings();

        // Salvar no servidor
        settings.SaveToDisk();
    }

    public CustomGameSettings CriarCustomGameSettings()
    {
        CustomGameSettings settings = new CustomGameSettings();
        settings.Professor = paginaEscolherProfessor.ProfessorSelecionado;
        settings.Sala = SalaDeAula.SalaDeCiencias;

        settings.ValorNivelDeEnsino = paginaInformacoesBasicas.NivelDeEnsinoSelecionado.valor;
        settings.ValorAreaDeConhecimento = paginaInformacoesBasicas.AreaDeConhecimentoSelecionada.valor;

        settings.IntroducaoAula = introducaoAula.text;
        settings.DescricaoMomento1 = descricaoMomento1.text;
        settings.DescricaoMomento2 = descricaoMomento2.text;
        settings.DescricaoMomento3 = descricaoMomento3.text;

        settings.Procedimento1 = paginaProcedimentoAgrupamento.ProcedimentoMomento1;
        settings.Procedimento2 = paginaProcedimentoAgrupamento.ProcedimentoMomento2;
        settings.Procedimento3 = paginaProcedimentoAgrupamento.ProcedimentoMomento3;

        settings.Agrupamento1 = paginaProcedimentoAgrupamento.AgrupamentoMomento1;
        settings.Agrupamento2 = paginaProcedimentoAgrupamento.AgrupamentoMomento2;
        settings.Agrupamento3 = paginaProcedimentoAgrupamento.AgrupamentoMomento3;

        settings.ArrayMidiaPoderFeedbackMomento1 = paginaFeedbackMidiasMomento1.ArrayMidiaPoderFeedback;
        settings.ArrayMidiaPoderFeedbackMomento2 = paginaFeedbackMidiasMomento2.ArrayMidiaPoderFeedback;
        settings.ArrayMidiaPoderFeedbackMomento3 = paginaFeedbackMidiasMomento3.ArrayMidiaPoderFeedback;

        settings.TituloDaAula = tituloDaAula.text;
        settings.Autor = autorInputField.text;

        // Definir a data de criação do jogo
        settings.dataDeCriacao = DateTime.Now.ToUniversalTime().AddHours(-3).ToShortDateString();

        return settings;
    }
}
