using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCustomGamePanel : MonoBehaviour {

    private CustomGameSettings currentSettings;

    // Campos relacionados à UI
    [SerializeField]
    private Image professorImage;

    // Use this for initialization
    void Start () {
        try
        {
            currentSettings = CustomGameSettings.ReadCustomGameSettingsFromDisk();

            // Mostrar professor selecionado
            var teacher = currentSettings.Professor;
            professorImage.sprite = CharacterSpriteDatabase.SpriteSW(teacher);
            professorImage.preserveAspect = true;

            // Se o sprite existe, alpha se torna igual a 1 e ele é mostrado
            if (professorImage.sprite)
                professorImage.color = new Color(1, 1, 1, 1);

            // Debug.Log(currentSettings.FalaProfessorSalaProfessores);
        }
        catch (System.Exception)
        {
            Debug.Log("Impossível ler o arquivo de jogo custom...");
        }
        
        
    }
}
