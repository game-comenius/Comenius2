using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class CreateCustomGamePanel : MonoBehaviour {

    [SerializeField]
    private Button ProfessorSelecionado;

    [SerializeField]
    private InputField falaProfessorSalaProfessores;
    [SerializeField]
    private Button submitButton;

    [SerializeField]
    private AvailableItemsPanel panelDeMidiasDisponiveis;
    

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

        settings.SaveCustomGameSettingsToDisk();
    }
}
