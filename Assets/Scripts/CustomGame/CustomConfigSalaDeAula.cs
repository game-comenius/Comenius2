using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomConfigSalaDeAula : MonoBehaviour
{

    void Awake()
    {
        // Destruir todas as portas, esta é a última sala do custom
        var doors = FindObjectsOfType<DoorTransition>();
        foreach (var door in doors) Destroy(door);
    }

    // Use this for initialization
    void Start()
    {
        ConfigurarSalaDeAula(CustomGameSettings.CurrentSettings);
    }

    private void ConfigurarSalaDeAula(CustomGameSettings settings)
    {
        var professor = FindObjectOfType<TeacherScript>().gameObject;
        var classManager = FindObjectOfType<ClassManager>();

        ConfigurarSpritesDoProfessor(professor, settings);
        ConfigurarFalasDoProfessorDuranteAula(classManager, settings);
        ConfigurarFalaDoProfessorPosAula(classManager, settings);
    }

    private void ConfigurarSpritesDoProfessor(GameObject professor, CustomGameSettings settings)
    {
        var teacherScript = professor.GetComponent<TeacherScript>();
        var professorName = settings.Professor;

        // Load dos sprites do professor selecionado pelo criador do custom
        var spriteNW = CharacterSpriteDatabase.SpriteNW(professorName);
        var spriteNE = CharacterSpriteDatabase.SpriteNE(professorName);
        var spriteSE = CharacterSpriteDatabase.SpriteSE(professorName);
        var spriteSW = CharacterSpriteDatabase.SpriteSW(professorName);

        // Inicialmente o professor vai olhar para o sudeste (SE)
        var mySP = teacherScript.GetComponent<SpriteRenderer>();
        mySP.sprite = spriteSE;

        // Configurar sprites no componente TeacherScript
        teacherScript.Sprites[0] = spriteSE;
        teacherScript.Sprites[1] = spriteSW;
        teacherScript.Sprites[2] = spriteNE;
        teacherScript.Sprites[3] = spriteNW;

        // Configurar sprites do professor caminhando
        // Por enquanto estamos usando os mesmos sprites dos professores parados
        // porque alguns professores do custom não tem sprites caminhando ainda
        for (int i = 0; i < teacherScript.GoLeft.Length; i++)
            teacherScript.GoLeft[i] = spriteSW;

        for (int i = 0; i < teacherScript.GoRight.Length; i++)
            teacherScript.GoRight[i] = spriteNE;
    }

    private void ConfigurarFalasDoProfessorDuranteAula(ClassManager classManager, CustomGameSettings settings)
    {
        var paragrafos = new string[3];
        paragrafos[0] = settings.DescricaoMomento1;
        paragrafos[1] = settings.DescricaoMomento2;
        paragrafos[2] = settings.DescricaoMomento3;

        // Adicionar falas ao diálogo do professor
        var falas = new GameComenius.Dialogo.Dialogo[paragrafos.Length];
        for (int i = 0; i < paragrafos.Length; i++)
        {
            falas[i] = new GameComenius.Dialogo.Dialogo();
            falas[i].nodulos = new GameComenius.Dialogo.DialogoNodulo[1];
            falas[i].nodulos[0] = new GameComenius.Dialogo.DialogoNodulo();
            falas[i].nodulos[0].falas = new GameComenius.Dialogo.Fala[1];
            falas[i].nodulos[0].falas[0] = new GameComenius.Dialogo.Fala();
            falas[i].nodulos[0].falas[0].fala = paragrafos[i];
            var p = Enum.Parse(typeof(GameComenius.Dialogo.Personagens), settings.Professor.ToString(), true);
            falas[i].nodulos[0].falas[0].personagem = (GameComenius.Dialogo.Personagens)p;
            falas[i].nodulos[0].falas[0].emocao = GameComenius.Dialogo.Expressao.Sorrindo;
        }
        classManager.Falas = falas;
    }

    private void ConfigurarFalaDoProfessorPosAula(ClassManager classManager, CustomGameSettings settings)
    {
        var dialogos = classManager.DialogosProfessorPosAula();

        foreach (var dialogo in dialogos)
        {
            var p = Enum.Parse(typeof(GameComenius.Dialogo.Personagens), settings.Professor.ToString(), true);
            // falas[1] porque o [0] é dedicada à Lurdinha
            // Se colocarem a fala do professor em falas[0], é só trocar
            if (dialogo.nodulos[0].falas.Length > 1)
            {
                dialogo.nodulos[0].falas[1].personagem = (GameComenius.Dialogo.Personagens)p;
                dialogo.nodulos[0].falas[1].emocao = GameComenius.Dialogo.Expressao.Sorrindo;
            }
            else
            {
                dialogo.nodulos[0].falas[0].personagem = (GameComenius.Dialogo.Personagens)p;
                dialogo.nodulos[0].falas[0].emocao = GameComenius.Dialogo.Expressao.Sorrindo;
            }
        }
    }
}