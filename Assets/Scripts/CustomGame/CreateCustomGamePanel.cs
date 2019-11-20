using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using TMPro;

public class CreateCustomGamePanel : MonoBehaviour {

    [SerializeField]
    private AvailableItemsPanel panelMidiasDisponiveis;

    private LinkedList<GameObject> paginas;
    private LinkedListNode<GameObject> nodoPaginaAtual;

    [SerializeField]
    private GameObject ListaFalasProfessor;
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

        // Coletar falas do professor na sala dos professores
        var falas = ListaFalasProfessor.GetComponentsInChildren<TMP_InputField>();
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

        settings.midiasDisponiveis = panelMidiasDisponiveis.MidiasSelecionadas();

        settings.SaveCustomGameSettingsToDisk();
    }
}
