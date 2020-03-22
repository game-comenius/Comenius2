using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Linq;

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

    // LinkedList não é serializable para o inspector, então popular um array
    // no inspector e no Start, jogar o array populado na LinkedList
    [SerializeField]
    private GameObject[] paginas;
    private LinkedList<GameObject> listaDePaginas;
    private LinkedListNode<GameObject> nodoPaginaAtual;

    // Campos para não ter que procurar depois
    private PaginaEscolherProfessor paginaEscolherProfessor;
    private PaginaEscolherSalaDeAula paginaEscolherSalaDeAula;
    private PaginaInformacoesBasicas paginaInformacoesBasicas;
    private PaginaEscolherMidias paginaEscolherMidias;
    private PaginaResumoSalvar paginaResumoSalvar;

    [SerializeField] private Button botaoVoltarPagina;
    [SerializeField] private Button botaoAvancarPagina;

    [SerializeField]
    private GameObject listaFalasProfessor;
    private TMP_InputField introducaoAula;
    private TMP_InputField descricaoMomento1;
    private TMP_InputField descricaoMomento2;
    private TMP_InputField descricaoMomento3;

    //private MomentoUICriarCustom momento1;
    //private MomentoUICriarCustom momento2;
    //private MomentoUICriarCustom momento3;

    //private EditarPoderMidiasScrollView[] editarPoderMidiasScrollViews;

    [SerializeField]
    private TMP_InputField tituloDaAula;
    [SerializeField]
    private TMP_InputField autorInputField;

    private void Awake()
    {
        // Pegar a lista de páginas pelas páginas definidas pelo inspector
        listaDePaginas = new LinkedList<GameObject>(paginas);

        // Ativar as páginas para buscar os scripts necessários
        foreach (var pagina in listaDePaginas) pagina.SetActive(true);
        paginaEscolherProfessor = GetComponentInChildren<PaginaEscolherProfessor>();
        paginaEscolherSalaDeAula = GetComponentInChildren<PaginaEscolherSalaDeAula>();
        paginaInformacoesBasicas = GetComponentInChildren<PaginaInformacoesBasicas>();
        paginaEscolherMidias = GetComponentInChildren<PaginaEscolherMidias>();
        paginaResumoSalvar = GetComponentInChildren<PaginaResumoSalvar>();

        //// Coletar falas do professor na sala dos professores
        var falas = listaFalasProfessor.GetComponentsInChildren<TMP_InputField>();
        introducaoAula = falas[0];
        descricaoMomento1 = falas[1];
        descricaoMomento2 = falas[2];
        descricaoMomento3 = falas[3];

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

    public void pressSubmit2() { Debug.Log("hehe"); }

    public void PressButtonThatStartsCustomGame()
    {
        // Criar objeto para escrever no disco
        CustomGameSettings settings = new CustomGameSettings();
        settings.Professor = paginaEscolherProfessor.ProfessorSelecionado;
        settings.Sala = paginaEscolherSalaDeAula.SalaSelecionada;

        //settings.NivelDeEnsino = paginaInformacoesBasicas.NivelDeEnsinoSelecionado.valor;
        //settings.AreaDeConhecimento = paginaInformacoesBasicas.AreaDeConhecimentoSelecionada.valor;

        settings.IntroducaoAula = introducaoAula.text;
        settings.DescricaoMomento1 = descricaoMomento1.text;
        settings.DescricaoMomento2 = descricaoMomento2.text;
        settings.DescricaoMomento3 = descricaoMomento3.text;

        //settings.Procedimento1 = momento1.ProcedimentoSelecionado;
        //settings.Procedimento2 = momento2.ProcedimentoSelecionado;
        //settings.Procedimento3 = momento3.ProcedimentoSelecionado;

        //settings.Agrupamento1 = momento1.AgrupamentoSelecionado;
        //settings.Agrupamento2 = momento2.AgrupamentoSelecionado;
        //settings.Agrupamento3 = momento3.AgrupamentoSelecionado;

        //settings.ArrayMidiaPoderFeedbackPorMomento = MidiaPoderFeedback3Momentos();

        settings.TituloDaAula = tituloDaAula.text;
        settings.Autor = autorInputField.text;

        //Debug.Log("Salvando jogo...\n" +
        //    "Professor = " + settings.Professor.NomeCompleto() +
        //    "\nSala = " + settings.Sala.NomeCompleto() +
        //    "\nNível De Ensino = " + NivelDeEnsino.Get(settings.NivelDeEnsino) +
        //    "\nÁrea de Conhecimento = " + AreaDeConhecimento.Get(settings.AreaDeConhecimento) +
        //    "\nIntrodução da Aula = " + settings.IntroducaoAula +
        //    "\nDescrição do Momento 1 = " + settings.DescricaoMomento1 +
        //    "\nDescrição do Momento 2 = " + settings.DescricaoMomento2 +
        //    "\nDescrição do Momento 3 = " + settings.DescricaoMomento3 +
        //    "\nTitulo da Aula = " + settings.TituloDaAula +
        //    "\nAutor = " + settings.Autor);

        // Mais tarde, salvar sim no servidor, por enquanto jogar diretamente
        // o jogo recém criado
        //settings.SaveToDisk();

        // Deletar o que há aqui abaixo depois, isso é temporário
        var midiasSelecionadas = paginaEscolherMidias.MidiasSelecionadas;
        var quantidadeMidias = midiasSelecionadas.Count;
        settings.ArrayMidiaPoderFeedbackMomento1 = new MidiaPoderFeedback[quantidadeMidias];
        settings.ArrayMidiaPoderFeedbackMomento2 = new MidiaPoderFeedback[quantidadeMidias];
        settings.ArrayMidiaPoderFeedbackMomento3 = new MidiaPoderFeedback[quantidadeMidias];
        // Momento 1
        for (int i = 0; i < quantidadeMidias; i++)
        {
            settings.ArrayMidiaPoderFeedbackMomento1[i] = new MidiaPoderFeedback();
            settings.ArrayMidiaPoderFeedbackMomento1[i].Midia = midiasSelecionadas[i];
            settings.ArrayMidiaPoderFeedbackMomento1[i].Poder = Poder.MuitoBoa;
            settings.ArrayMidiaPoderFeedbackMomento1[i].Feedback = "Incrível!";
        }
        // Momento 2
        for (int i = 0; i < quantidadeMidias; i++)
        {
            settings.ArrayMidiaPoderFeedbackMomento2[i] = new MidiaPoderFeedback();
            settings.ArrayMidiaPoderFeedbackMomento2[i].Midia = midiasSelecionadas[i];
            settings.ArrayMidiaPoderFeedbackMomento2[i].Poder = Poder.MuitoBoa;
            settings.ArrayMidiaPoderFeedbackMomento2[i].Feedback = "Incrível!";
        }
        // Momento 3
        for (int i = 0; i < quantidadeMidias; i++)
        {
            settings.ArrayMidiaPoderFeedbackMomento3[i] = new MidiaPoderFeedback();
            settings.ArrayMidiaPoderFeedbackMomento3[i].Midia = midiasSelecionadas[i];
            settings.ArrayMidiaPoderFeedbackMomento3[i].Poder = Poder.MuitoBoa;
            settings.ArrayMidiaPoderFeedbackMomento3[i].Feedback = "Incrível!";
        }
        CustomGameSettings.CurrentSettings = settings;
    }

    //private MidiaPoderFeedback[][] MidiaPoderFeedback3Momentos()
    //{
    //    var mpfs = new MidiaPoderFeedback[3][];
    //    mpfs[0] = MidiaPoderFeedbackMomento(0);
    //    mpfs[1] = MidiaPoderFeedbackMomento(1);
    //    mpfs[2] = MidiaPoderFeedbackMomento(2);
    //    return mpfs;
    //}

    //private MidiaPoderFeedback[] MidiaPoderFeedbackMomento(int momento)
    //{
    //    var faixasMomento = editarPoderMidiasScrollViews[momento].FaixasEditarPoderMidia;
    //    var quantidadeMidias = faixasMomento.Count;
    //    var mpfs = new MidiaPoderFeedback[quantidadeMidias];
    //    for (int i = 0; i < quantidadeMidias; i++)
    //    {
    //        mpfs[i] = new MidiaPoderFeedback();
    //        mpfs[i].Midia = faixasMomento[i].Midia;
    //        mpfs[i].Poder = faixasMomento[i].Poder;
    //        mpfs[i].Feedback = faixasMomento[i].Feedback();
    //    }

    //    return mpfs;
    //}
}
