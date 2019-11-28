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



            // === DEBUG ===
            //var a = currentSettings.nivelDeEnsino;
            //var b = currentSettings.areaDeConhecimento;
            //Debug.Log("nível = " + a.nome + ", área = " + b.nome);
            //var a = currentSettings.arrayMidiaPoderFeedbackPorMomento;
            //foreach (var arrayMPF in a)
            //{
            //    foreach (var mpf in arrayMPF)
            //    {
            //        var s = mpf.Midia + " - poder: " + mpf.Poder + " - Feedback: ";
            //        //s += "length = " + mpf.Feedback.Length + "valor = ";
            //        //foreach (var c in s.ToCharArray())
            //        //{
            //        //    s += (int)c + " / ";
            //        //}
            //        s += mpf.Feedback;

            //        Debug.Log(s);
            //    }
            //}
            // ============
        }
        catch (System.Exception)
        {
            Debug.Log("Impossível ler o arquivo de jogo custom...");
        }
        
        
    }
}
