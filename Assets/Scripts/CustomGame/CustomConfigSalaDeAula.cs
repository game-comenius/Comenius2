using System;
using System.Linq;
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
        var otherDoors = GameObject.FindGameObjectsWithTag("Door1");
        foreach (var door in otherDoors) Destroy(door.gameObject);

        // Para impedir que o professor ande, nem todos tem sprite para andar
        var teacherScripts = FindObjectsOfType<TeacherScript>();
        foreach (var teacherScript in teacherScripts) teacherScript.CanWalk = false;

    }

    private IEnumerator Start()
    {
        ConfigurarSalaDeAula(CustomGameSettings.CurrentSettings);

        // Remover missão "Ir para a aula" da janela de missões assim que
        // a Lurdinha chega na sala de aula
        var questIrParaAula = CustomConfigSalaProfessores.questIrParaAula;
        if (questIrParaAula != null)
        {
            yield return new WaitUntil(() => ConselheiroComenius.JanelaMissoes != null);
            ConselheiroComenius.JanelaMissoes.RemoverMissao(questIrParaAula);
        }
    }

    private void ConfigurarSalaDeAula(CustomGameSettings settings)
    {
        var professor = FindObjectOfType<TeacherScript>().gameObject;
        var classManager = FindObjectOfType<ClassManager>();

        ConfigurarSpritesDoProfessor(professor, settings);
        ConfigurarFalasDoProfessorDuranteAula(classManager, settings);
        ConfigurarFalaDoProfessorPosAula(classManager, settings);

        // Configurar falas de feedback dos alunos quando a aula terminar
        ClassManager.EndClass += () => ConfigurarFalasDeFeedback(classManager, settings);
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

        // Configurar sprites no componente TeacherScript
        teacherScript.Sprites[0] = spriteSE;
        teacherScript.Sprites[1] = spriteSW;
        teacherScript.Sprites[2] = spriteNE;
        teacherScript.Sprites[3] = spriteNW;

        // Inicialmente o professor vai olhar para o sudeste (SE)
        var mySP = teacherScript.GetComponent<SpriteRenderer>();
        mySP.sprite = spriteSE;

        // Alguns casos especiais, não quero mexer no .png
        // Basicamente, alterar o tamanho de alguns professores
        var professorTransform = professor.transform;
        switch (professorName)
        {
            case CharacterName.Vladmir:
            case CharacterName.Paulino:
                professorTransform.localScale = Vector3.one * 0.7f;
                break;
        }


        // Configurar sprites do professor caminhando
        // Por enquanto estamos usando os mesmos sprites dos professores parados
        // porque alguns professores do custom não tem sprites caminhando ainda
        //for (int i = 0; i < teacherScript.GoLeft.Length; i++)
        //    teacherScript.GoLeft[i] = spriteSW;

        //for (int i = 0; i < teacherScript.GoRight.Length; i++)
        //    teacherScript.GoRight[i] = spriteNE;
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

    private void ConfigurarFalasDeFeedback(ClassManager classManager, CustomGameSettings settings)
    {
        // Configurar feedback geral sobre a aula do professor também?
        // ...

        // Configurar falas de feedback específicas sobre as mídias
        // Para cada momento, um aluno na sala falará sobre a mídia escolhida
        // para aquele momento da aula

        // Obter feedbacks escritos pelo criador do custom
        CreateCustomGamePanel.MidiaPoderFeedback[][] arraysMPF =
        {
            settings.ArrayMidiaPoderFeedbackMomento1,
            settings.ArrayMidiaPoderFeedbackMomento2,
            settings.ArrayMidiaPoderFeedbackMomento3,
        };
        foreach (var arrayMPF in arraysMPF) if (arrayMPF == null) return;

        // Criar 3 Dictionary, um para cada momento, para guardarem o link entre
        // uma mídia e o seu feedback
        var feedbacksPorMidiaNoMomento1Custom = new Dictionary<ItemName, string>();
        var feedbacksPorMidiaNoMomento2Custom = new Dictionary<ItemName, string>();
        var feedbacksPorMidiaNoMomento3Custom = new Dictionary<ItemName, string>();
        Dictionary<ItemName, string>[] feedbacksPorMidiaArray =
        {
            feedbacksPorMidiaNoMomento1Custom,
            feedbacksPorMidiaNoMomento2Custom,
            feedbacksPorMidiaNoMomento3Custom
        };

        // Guardar as informações escritas pelo criador nos Dictionary
        // Depois, esses Dictionary serão usados para instanciar um objeto da
        // classe FeedbacksDosAlunos
        for (var momento = 0; momento < 3; momento++)
        {
            var quantidadeMidias = arraysMPF[momento].Length;
            for (var j = 0; j < quantidadeMidias; j++)
            {
                var mpf = arraysMPF[momento][j];
                feedbacksPorMidiaArray[momento][mpf.Midia] = mpf.Feedback;
            }
        }

        // Configurar falas de feedback gerais sobre a aula
        string[] feedbacksAulaMelhor = { "A aula foi incrível!" };
        string[] feedbacksAulaMuitoBoa = { "A aula foi muito boa!" };
        string[] feedbacksAulaBoa = { "A aula foi boa!" };
        string[] feedbacksAulaFraca = { "A aula foi ruim..." };

        // Criar novo objeto FeedbacksDosAlunos, que conterá falas específicas
        // sobre as mídias e falas gerais sobre a aula de acordo com as escolhas
        // feitas pelo criador da missão custom
        FeedbacksDosAlunos feedbacksDosAlunos = new FeedbacksDosAlunos
        (
            feedbacksPorMidiaNoMomento1Custom,
            feedbacksPorMidiaNoMomento2Custom,
            feedbacksPorMidiaNoMomento3Custom,
            feedbacksAulaMelhor,
            feedbacksAulaMuitoBoa,
            feedbacksAulaBoa,
            feedbacksAulaFraca
        );

        // Pedir para o ClassManager configurar as falas dos alunos
        // comentaristas com os feedbacks custom
        classManager.AlunosComentaristasSetUp(feedbacksDosAlunos);
    }
}