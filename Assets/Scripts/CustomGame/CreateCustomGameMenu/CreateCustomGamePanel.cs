using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using TMPro;
using System;

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


    private NivelDeEnsinoDropdown dropdownNivelDeEnsino;
    private AreaDeConhecimentoDropdown dropdownAreaDeConhecimento;

    // LinkedList não é serializable para o inspector, então popular um array
    // no inspector e no Start, jogar o array populado na LinkedList
    [SerializeField]
    private GameObject[] paginas;
    private LinkedList<GameObject> listaDePaginas;
    private LinkedListNode<GameObject> nodoPaginaAtual;

    [SerializeField] private Button botaoVoltarPagina;
    [SerializeField] private Button botaoAvancarPagina;

    [SerializeField]
    private GameObject listaFalasProfessor;
    private TMP_InputField introducaoAula;
    private TMP_InputField descricaoMomento1;
    private TMP_InputField descricaoMomento2;
    private TMP_InputField descricaoMomento3;

    private MomentoUICriarCustom momento1;
    private MomentoUICriarCustom momento2;
    private MomentoUICriarCustom momento3;

    private EditarPoderMidiasScrollView[] editarPoderMidiasScrollViews;

    [SerializeField]
    private TMP_InputField tituloDaAula;
    [SerializeField]
    private TMP_InputField autorInputField;

    private void Awake()
    {
        // Pegar a lista de páginas pelas páginas definidas pelo inspector
        listaDePaginas = new LinkedList<GameObject>(paginas);

        //// Ativar todas as páginas para coletar botões, campos, ...
        //foreach (var pagina in listaDePaginas) pagina.SetActive(true);

        //// Coletar dropdowns do nível de ensino e a área de conhecimento
        //dropdownNivelDeEnsino = GetComponentInChildren<NivelDeEnsinoDropdown>();
        //dropdownAreaDeConhecimento = GetComponentInChildren<AreaDeConhecimentoDropdown>();

        //// Coletar falas do professor na sala dos professores
        //var falas = listaFalasProfessor.GetComponentsInChildren<TMP_InputField>();
        //introducaoAula = falas[0];
        //descricaoMomento1 = falas[1];
        //descricaoMomento2 = falas[2];
        //descricaoMomento3 = falas[3];

        //// Coletar os momentos que contém as configurações de proc. e agrup.
        //var momentos = this.GetComponentsInChildren<MomentoUICriarCustom>();
        //momento1 = momentos[0];
        //momento2 = momentos[1];
        //momento3 = momentos[2];

        //editarPoderMidiasScrollViews = GetComponentsInChildren<EditarPoderMidiasScrollView>();

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

    public void PressSubmitButton()
    {
        // Criar objeto para escrever no disco
        CustomGameSettings settings = new CustomGameSettings();
        settings.Professor = GetComponentInChildren<PaginaEscolherProfessor>().ProfessorSelecionado;
        // Alterar para a escolha do jogador
        settings.Sala = SalaDeAula.Jean;

        settings.NivelDeEnsino = dropdownNivelDeEnsino.NivelDeEnsinoSelecionado().valor;
        settings.AreaDeConhecimento = dropdownAreaDeConhecimento.AreaDeConhecimentoSelecionada().valor;

        settings.IntroducaoAula = introducaoAula.text;
        settings.DescricaoMomento1 = descricaoMomento1.text;
        settings.DescricaoMomento2 = descricaoMomento2.text;
        settings.DescricaoMomento3 = descricaoMomento3.text;

        settings.Procedimento1 = momento1.ProcedimentoSelecionado;
        settings.Procedimento2 = momento2.ProcedimentoSelecionado;
        settings.Procedimento3 = momento3.ProcedimentoSelecionado;

        settings.Agrupamento1 = momento1.AgrupamentoSelecionado;
        settings.Agrupamento2 = momento2.AgrupamentoSelecionado;
        settings.Agrupamento3 = momento3.AgrupamentoSelecionado;

        settings.ArrayMidiaPoderFeedbackPorMomento = MidiaPoderFeedback3Momentos();

        settings.TituloDaAula = tituloDaAula.text;
        settings.Autor = autorInputField.text;

        settings.SaveToDisk();
    }

    private MidiaPoderFeedback[][] MidiaPoderFeedback3Momentos()
    {
        var mpfs = new MidiaPoderFeedback[3][];
        mpfs[0] = MidiaPoderFeedbackMomento(0);
        mpfs[1] = MidiaPoderFeedbackMomento(1);
        mpfs[2] = MidiaPoderFeedbackMomento(2);
        return mpfs;
    }

    private MidiaPoderFeedback[] MidiaPoderFeedbackMomento(int momento)
    {
        var faixasMomento = editarPoderMidiasScrollViews[momento].FaixasEditarPoderMidia;
        var quantidadeMidias = faixasMomento.Count;
        var mpfs = new MidiaPoderFeedback[quantidadeMidias];
        for (int i = 0; i < quantidadeMidias; i++)
        {
            mpfs[i] = new MidiaPoderFeedback();
            mpfs[i].Midia = faixasMomento[i].Midia;
            mpfs[i].Poder = faixasMomento[i].Poder;
            mpfs[i].Feedback = faixasMomento[i].Feedback();
        }

        return mpfs;
    }
}
