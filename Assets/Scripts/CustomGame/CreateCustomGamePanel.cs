using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class CreateCustomGamePanel : MonoBehaviour {

    [SerializeField]
    private InputField falaProfessorSalaProfessores;

    [SerializeField]
    private AvailableItemsPanel panelMidiasDisponiveis;

    [SerializeField]
    private MomentoUICriarCustom momento1;
    [SerializeField]
    private MomentoUICriarCustom momento2;
    [SerializeField]
    private MomentoUICriarCustom momento3;


    [SerializeField]
    private List<GameObject> paginas;
    private List<GameObject>.Enumerator paginasEnumerator;

    private void Start()
    {
        // Desativar todas as páginas deste panel e ativar apenas a primeira
        foreach (var pagina in paginas) pagina.SetActive(false);
        paginasEnumerator = paginas.GetEnumerator();
        if (paginasEnumerator.MoveNext())
            paginasEnumerator.Current.SetActive(true);
    }



    // Métodos vinculados a botões
    public void IrParaProximaPagina()
    {
        var paginaAnterior = paginasEnumerator.Current;
        if (paginasEnumerator.MoveNext())
        {
            paginaAnterior.SetActive(false);
            paginasEnumerator.Current.SetActive(true);
        }
    }

    public void PressSubmitButton()
    {
        // Criar objeto para escrever no disco
        CustomGameSettings settings = new CustomGameSettings();
        settings.Professor = SelectProfessorButton.CurrentlySelectedButton.Professor;
        settings.FalaProfessorSalaProfessores = falaProfessorSalaProfessores.text;
        settings.midiasDisponiveis = panelMidiasDisponiveis.MidiasSelecionadas();

        settings.Procedimento1 = momento1.ProcedimentoSelecionado;
        settings.Procedimento2 = momento2.ProcedimentoSelecionado;
        settings.Procedimento3 = momento3.ProcedimentoSelecionado;

        settings.SaveCustomGameSettingsToDisk();
    }
}
