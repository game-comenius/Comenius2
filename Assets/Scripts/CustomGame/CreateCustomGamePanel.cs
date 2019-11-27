using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class CreateCustomGamePanel : MonoBehaviour {

    // struct útil para transferir as informações criadas para o disco
    // e depois resgatá-las para a memória durante o load de um jogo criado
    [Serializable()]
    public struct MidiaPoderFeedback
    {
        public ItemName Midia;
        public Poder Poder;
        public string Feedback;
    }

    private LinkedList<GameObject> paginas;
    private LinkedListNode<GameObject> nodoPaginaAtual;

    private NivelDeEnsinoDropdown dropdownNivelDeEnsino;
    private AreaDeConhecimentoDropdown dropdownAreaDeConhecimento;

    [SerializeField]
    private GameObject listaFalasProfessor;
    private TMP_InputField introducaoAula;
    private TMP_InputField descricaoMomento1;
    private TMP_InputField descricaoMomento2;
    private TMP_InputField descricaoMomento3;

    private MomentoUICriarCustom momento1;
    private MomentoUICriarCustom momento2;
    private MomentoUICriarCustom momento3;

    private void Awake()
    {
        // Achar e catalogar todas as páginas do Panel, se o Panel tiver um
        // filho, este filho deve ser uma página para criar um jogo custom
        paginas = new LinkedList<GameObject>();
        var numeroDePaginas = this.transform.childCount;
        for (int i = 0; i < numeroDePaginas; i++)
        {
            var pagina = this.transform.GetChild(i).gameObject;
            paginas.AddLast(pagina);
        }

        // Ativar todas as páginas para coletar botões, campos, ...
        foreach (var pagina in paginas) pagina.SetActive(true);

        // Coletar dropdowns do nível de ensino e a área de conhecimento
        dropdownNivelDeEnsino = GetComponentInChildren<NivelDeEnsinoDropdown>();
        dropdownAreaDeConhecimento = GetComponentInChildren<AreaDeConhecimentoDropdown>();

        // Coletar falas do professor na sala dos professores
        var falas = listaFalasProfessor.GetComponentsInChildren<TMP_InputField>();
        introducaoAula = falas[0];
        descricaoMomento1 = falas[1];
        descricaoMomento2 = falas[2];
        descricaoMomento3 = falas[3];

        // Coletar os momentos que contém as configurações de proc. e agrup.
        var momentos = this.GetComponentsInChildren<MomentoUICriarCustom>();
        momento1 = momentos[0];
        momento2 = momentos[1];
        momento3 = momentos[2];

        
        // Desativar todas as páginas deste panel e ativar apenas a primeira
        foreach (var pagina in paginas) pagina.SetActive(false);
        nodoPaginaAtual = paginas.First;
        nodoPaginaAtual.Value.SetActive(true);
    }



    // Métodos vinculados a botões
    public void IrParaProximaPagina()
    {
        var paginaAnterior = nodoPaginaAtual.Value;
        paginaAnterior.SetActive(false);

        nodoPaginaAtual = nodoPaginaAtual.Next;
        nodoPaginaAtual.Value.SetActive(true);
    }

    public void IrParaPaginaAnterior()
    {
        var paginaSeguinte = nodoPaginaAtual.Value;
        paginaSeguinte.SetActive(false);

        nodoPaginaAtual = nodoPaginaAtual.Previous;
        nodoPaginaAtual.Value.SetActive(true);
    }

    public void PressSubmitButton()
    {
        // Criar objeto para escrever no disco
        CustomGameSettings settings = new CustomGameSettings();
        settings.Professor = SelectProfessorButton.CurrentlySelectedButton.Professor;

        settings.nivelDeEnsino = dropdownNivelDeEnsino.NivelDeEnsinoSelecionado();
        settings.areaDeConhecimento = dropdownAreaDeConhecimento.AreaDeConhecimentoSelecionada();

        settings.introducaoAula = introducaoAula.text;
        settings.descricaoMomento1 = descricaoMomento1.text;
        settings.descricaoMomento2 = descricaoMomento2.text;
        settings.descricaoMomento3 = descricaoMomento3.text;

        settings.Procedimento1 = momento1.ProcedimentoSelecionado;
        settings.Procedimento2 = momento2.ProcedimentoSelecionado;
        settings.Procedimento3 = momento3.ProcedimentoSelecionado;

        settings.Agrupamento1 = momento1.AgrupamentoSelecionado;
        settings.Agrupamento2 = momento2.AgrupamentoSelecionado;
        settings.Agrupamento3 = momento3.AgrupamentoSelecionado;

        settings.arrayMidiaPoderFeedbackPorMomento = MidiaPoderFeedback3Momentos();

        settings.SaveCustomGameSettingsToDisk();
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
        var editarPoderMidiasScrollViews = GetComponentsInChildren<EditarPoderMidiasScrollView>(true);

        // Coletar feedbacks para o momento 1
        var faixasMomento1 = editarPoderMidiasScrollViews[momento].FaixasEditarPoderMidia;
        var quantidadeMidias = faixasMomento1.Count;
        var mpfs = new MidiaPoderFeedback[quantidadeMidias];
        for (int i = 0; i < quantidadeMidias; i++)
        {
            mpfs[i] = new MidiaPoderFeedback();
            mpfs[i].Midia = faixasMomento1[i].Midia;
            mpfs[i].Poder = faixasMomento1[i].poder;
            mpfs[i].Feedback = faixasMomento1[i].Feedback();
        }

        return mpfs;
    }
}
