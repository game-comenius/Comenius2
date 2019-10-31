using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class ChooseCustomGamePanel : MonoBehaviour {

    [SerializeField]
    private Sprite jeanSprite;
    [SerializeField]
    private Sprite montanariSprite;
    [SerializeField]
    private Sprite alunoEstranhoSprite;

    [SerializeField]
    private Image professorImage;

    private CustomGameSettings currentSettings;

	// Use this for initialization
	void Start () {
        try
        {
            currentSettings = CustomGameLoadedSettings.Settings;

            // Mostrar professor selecionado
            switch (currentSettings.Professor)
            {
                case 0:
                    professorImage.sprite = jeanSprite;
                    break;
                case 1:
                    professorImage.sprite = montanariSprite;
                    break;
                case 2:
                    professorImage.sprite = alunoEstranhoSprite;
                    break;
                default:
                    break;
            }

            Debug.Log(currentSettings.FalaDoProfessor);
        }
        catch (System.Exception)
        {
            Debug.Log("Impossível ler o arquivo de jogo custom...");
        }
        
        if (professorImage.sprite)
            professorImage.color = new Color(1, 1, 1, 1);
    }
}
