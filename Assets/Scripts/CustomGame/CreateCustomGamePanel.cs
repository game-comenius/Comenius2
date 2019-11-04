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


    //private void Start()
    //{
    //    SelectTeacher(ProfessorSelecionado);
    //}

    //public void SelectTeacher(Button teacher)
    //{
    //    ProfessorSelecionado.image.color = new Color(0.6483624f, 0.6640738f, 0.8867924f, 0.8f);
    //    teacher.image.color = new Color(0.700088f, 0.8862745f, 0.6470588f);
    //    ProfessorSelecionado = teacher;
    //}

    public void PressSubmitButton()
    {
        // Criar objeto para escrever no disco
        CustomGameSettings settings = new CustomGameSettings();
        settings.Professor = SelectProfessorButton.CurrentlySelectedButton.Professor;
        settings.FalaProfessorSalaProfessores = falaProfessorSalaProfessores.text;

        settings.SaveCustomGameSettingsToDisk();
    }
}
