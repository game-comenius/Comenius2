using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameComenius.Dialogo;
using System;
using Random = UnityEngine.Random;

public class ClassManager : MonoBehaviour
{
    #region Geral
    static public ClassManager classManager { get; private set; }

    static private List<StudentScript> students = new List<StudentScript>();

    static private List<bool> studentIsProblem = new List<bool>();

    static private List<ProblemCloudScript> clouds = new List<ProblemCloudScript>();

    public delegate void ClassEnded();

    static public event ClassEnded EndClass;

    //[SerializeField] private Sprite[] alunos = new Sprite[12];  //Retirado porque o reposicionamento aleatório dos alunos não está dando certo devido às diferenças de tamanhos dos alunos e suas sprites

    [SerializeField] private GameObject cloud;

    [SerializeField] private Canvas canvas;

    [SerializeField] private Camera mainCamera;

    [SerializeField] private GameObject fader;

    public GameObject AgrupamentoIndividual;
    public GameObject AgrupamentoDuplas;
    public GameObject AgrupamentoGrandeGrupo;

    #endregion

    #region MomentoAula
    [Header("Class Moment")]

    //Var de debug.
    [SerializeField] Text timer;

    [Tooltip("Se refere a quanto tempo tem-se que esperar para a aula começar.")]
    [SerializeField] private float classWait;

    [Tooltip ("A componente \"X\" se refere à duração de cada momento da aula, \"Y\" ao  tempo que se tem que esperar para surgir o primeiro problema e \"Z\" ao tempo que tem que sobrar quando surgir o último problema. Tudo em segundos")]
    [SerializeField] private Vector3[] momentsTime = new Vector3[3];

    [Tooltip ("x é o ponto e y é a quantidade de problemas")]
    [SerializeField] private Vector2Int[] traducaoPontoProblema = new Vector2Int[4];

    [Tooltip("Se refere a quantidade de problemas cada momento da aula terá.")]
    private int[] problemQuantity = new int[3];

    [Tooltip("Aceleração do tempo quando não houverem problemas")]
    [SerializeField] [Range(1, 300)] private float timeAcceleration;

    [Tooltip("Aceleração do tempo quando houverem problemas")]
    [SerializeField] [Range(1, 300)] private float problemTimeAcceleration;

    private float _timeAcceleration; // aceleração que está sendo usada

    [SerializeField] private float problemDuration;

    [Tooltip("Espera entre o início dos problemas")]
    [SerializeField] private float waitBetweenProblems;

    private int classMoment = 0;

    private float momentTimer = 0;

    private float previousProblemTime = 0;

    private float nextProblemTime = 0;

    private Coroutine startClassCoroutine;

    [Tooltip ("Falas que todo professor que iniciarão cada momento de aula.")]
    [SerializeField] private Dialogo[] falas = new Dialogo[3];

    public Dialogo[] Falas
    {
        get { return falas; }
        set { falas = value; }
    }

    #endregion

    #region Momento Pós-Aula
    [Header("Post-Class Moment")]

    public GameObject AgrupamentoFimDeAula;

    [Tooltip("Os dois primeiros são os generalistas. O resto, em ordem, correspondem a cada momento de aula.")]
    [SerializeField] private StudentScript[] alunosComentaristas = new StudentScript[5];

    [SerializeField] private ComentarioProfessor professor = new ComentarioProfessor();

    public Dialogo[] DialogosProfessorPosAula()
    {
        var quantidade = professor.falasGeneralistas.Length;
        var dialogos = new Dialogo[quantidade];
        for (int i = 0; i < quantidade; i++)
            dialogos[i] = professor.falasGeneralistas[i].dialogos[0];
        return dialogos;
    }

    [Tooltip("O elemento 0 corresponde a Tier 1, e1 - t2, e2 - t3 e e3 - t4.")]
    [SerializeField] public DialogoGeneralista[] falasGeneralistas = new DialogoGeneralista[4];

    [SerializeField] public FalasSobreMomentos[] falasSobreMomentos = new FalasSobreMomentos[3];

    #region Classes

    [System.Serializable]
    public class FalaSobreMidias
    {
        public ItemName item = ItemName.Caderno;

        public Dialogo dialogo = new Dialogo();
    }

    [System.Serializable] public class FalasSobreMomentos
    {
        public FalaSobreMidias[] falaSobreMidias = new FalaSobreMidias[13];

        public Dialogo EncontrarFalaCerta(ItemName item)
        {
            foreach (FalaSobreMidias f in falaSobreMidias)
            {
                if (f.item == item)
                {
                    return f.dialogo;
                }
            }

            return falaSobreMidias[falaSobreMidias.Length - 1].dialogo;
        }
    }

    [System.Serializable] private class ComentarioProfessor
    {
        public TeacherScript professor;

        public DialogoGeneralista[] falasGeneralistas = new DialogoGeneralista[4];
    }

    [System.Serializable] public class DialogoGeneralista
    {
        public Vector2 rangeNota = Vector2.zero;

        public Dialogo[] dialogos = new Dialogo[2];
    }

    private int quantidadeDeAlunosQueDeramSeusFeedbacks;

    #endregion
    #endregion

    private void Awake()
    {
        if (classManager == null)
        {
            classManager = this;
        }
        else
        {
            Destroy(this);

            Debug.Log(gameObject.name + "tem um segundo ClassManager");
        }
    }

    private void Start()
    {
        //Inicializa a aula.
        _timeAcceleration = timeAcceleration;

        startClassCoroutine = StartCoroutine(StartClass());

        for (int i = 0; i < problemQuantity.Length; i++) 
        {
            problemQuantity[i] = TraduzirPontoProblema((int)Player.Instance.MissionHistory[Player.Instance.missionID].points[i]);
            Player.Instance.SomarProblemasTotais(problemQuantity[i]);
        }

        TeacherSetUp();

        AlunosComentaristasSetUp();

        //Configura o final da aula.
        EndClass += EndClassFunction;
    }

    // Para ajudar o desenvolvimento, se estivermos no editor, basta apertar
    // Esc para pular a parte do minigame durante a aula
    #if UNITY_EDITOR
    private void Update()
    {
        if (startClassCoroutine != null && Input.GetKeyDown(KeyCode.Escape))
        {
            StopCoroutine(startClassCoroutine);

            professor.professor.FimDaAula();

            // Retirar todas as mídias da sala de aula
            var midiaNaSalaDeAula = FindObjectOfType<MidiaNaSalaDeAula>();
            if (midiaNaSalaDeAula) midiaNaSalaDeAula.EsconderMidiaAtual();

            // Alterar a sala de acordo com a pontuação do jogador
            Desempenho.instance.TrocarSala();

            GameManager.UINaoSendoUsada();

            if (EndClass != null)
            {
                EndClass();
            }
        }
    }
    #endif

    private void OnDestroy()
    {
        EndClass -= EndClassFunction;
    }

    private void TeacherSetUp()
    {
        professor.professor.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        NpcDialogo dialogo = professor.professor.gameObject.GetComponent<NpcDialogo>();

        dialogo.dialogoObrigatorio = true;
        dialogo.enabled = false;

        for (int j = 0; j < professor.falasGeneralistas.Length; j++)
        {
            if (Player.Instance.MissionHistory[Player.Instance.missionID].totalMissionPoints >= falasGeneralistas[j].rangeNota.x 
                && Player.Instance.MissionHistory[Player.Instance.missionID].totalMissionPoints <= falasGeneralistas[j].rangeNota.y)
            {
                // Adicionar fala de despedida e recomendação à Lurdinha para
                // que converse com os alunos após esta conversa (pós-aula)
                var falaDespedida = "Neste momento, posso ver que os alunos estão falando sobre a aula, você pode conversar com eles para saber suas opiniões.";
                // Extrair o array de falas do professor
                var falas = professor.falasGeneralistas[j].dialogos[0].nodulos[0].falas;
                // Redimensionar o array para aceitar mais um paragrafo de fala
                Array.Resize(ref falas, falas.Length + 1);

                var ultimoIndice = falas.Length - 1;
                falas[ultimoIndice] = new Fala
                {
                    fala = falaDespedida,
                    personagem = falas[ultimoIndice - 1].personagem,
                    emocao = falas[ultimoIndice - 1].emocao
                };

                // Encaixar o array com as (falas originais + fala nova) no
                // conjunto de falas do professor
                dialogo.dialogoPrincipal = professor.falasGeneralistas[j].dialogos[0].Clone();
                dialogo.dialogoPrincipal.nodulos[0].falas = falas;
            }
        }
    }

    private void AlunosComentaristasSetUp()
    {
        // Obter o conjunto de feedbacks da missão atual
        var missaoAtual = Player.Instance.missionID;
        FeedbacksDosAlunos feedbacks;
        switch (missaoAtual)
        {
            default:
                feedbacks = FeedbacksDosAlunos.FeedbacksNaMissao1;
                break;
            case 1:
                feedbacks = FeedbacksDosAlunos.FeedbacksNaMissao2;
                break;
            case 2:
                feedbacks = FeedbacksDosAlunos.FeedbacksNaMissao3;
                break;
        }

        var alunosComComentariosGerais = 0;
        var alunosComComentariosSobreMidias = 0;

        // Obter feedbacks gerais sobre a aula
        var pontuacaoDaAula = Player.Instance.MissionHistory[missaoAtual].totalMissionPoints;
        var feedbacksGerais = feedbacks.ObterFeedbacksGeraisDaAula(pontuacaoDaAula);

        // Dar os feedbacks corretos para os alunos na sala de aula
        for (int i = 0; i < alunosComentaristas.Length; i++)
        {
            NpcDialogo npcDialogoComponent = alunosComentaristas[i].GetComponent<NpcDialogo>();

            // Se i <= 1, distribuir feedbacks gerais sobre a aula
            if (i <= 1)
            {
                // Construção de um diálogo para o aluno i
                Dialogo d = new Dialogo();
                d.nodulos = new DialogoNodulo[1];
                d.nodulos[0] = new DialogoNodulo();
                d.nodulos[0].falas = new Fala[1];
                d.nodulos[0].falas[0] = new Fala();
                d.nodulos[0].falas[0].emocao = Expressao.Serio;
                d.nodulos[0].falas[0].personagem = Personagens.Aluno;
                // Se não há falas novas para o aluno, usar fala 0
                if (alunosComComentariosGerais >= feedbacksGerais.Length)
                    d.nodulos[0].falas[0].fala = feedbacksGerais[0];
                else
                    d.nodulos[0].falas[0].fala = feedbacksGerais[alunosComComentariosGerais];
                npcDialogoComponent.dialogoPrincipal = d;

                alunosComComentariosGerais++;
            }
            // Se i > 1, distribuir feedbacks específicos sobre as mídias
            else
            {
                npcDialogoComponent.dialogoPrincipal = falasSobreMomentos[i - 2].EncontrarFalaCerta(Player.Instance.MissionHistory[Player.Instance.missionID].chosenMedia[i - 2]).Clone();
            }

            npcDialogoComponent.OnEndDialogueEvent += AceitarFeedbackDeUmAluno;
        }
    }

    private void AceitarFeedbackDeUmAluno()
    {
        quantidadeDeAlunosQueDeramSeusFeedbacks++;

        if (quantidadeDeAlunosQueDeramSeusFeedbacks >= alunosComentaristas.Length)
        {
            StartCoroutine(DispararFalaDoProfessorAposFeedbackDeTodosOsAlunos());
        }
    }

    private IEnumerator DispararFalaDoProfessorAposFeedbackDeTodosOsAlunos()
    {
        // Esperar até que a fala possa aparecer
        yield return new WaitWhile(() => GameManager.uiSendoUsada);

        // Criar o diálogo que o professor vai falar após os feedbacks
        Dialogo dialogo = new Dialogo();
        dialogo.nodulos = new DialogoNodulo[1];
        dialogo.nodulos[0] = new DialogoNodulo();
        dialogo.nodulos[0].falas = new Fala[2];
        // Adicionar fala da Lurdinha (em branco por enquanto, mas o código
        // está pronto aqui caso queiram adicionar uma fala da Lurdinha)
        dialogo.nodulos[0].falas[0] = new Fala
        {
            fala = "",
            personagem = GameComenius.Dialogo.Personagens.Lurdinha,
            emocao = GameComenius.Dialogo.Expressao.Sorrindo
        };
        // Adicionar fala do professor
        var npcDialogoProfessor = professor.professor.GetComponent<NpcDialogo>();
        dialogo.nodulos[0].falas[1] = new Fala
        {
            personagem = npcDialogoProfessor.dialogoPrincipal.nodulos[0].falas[1].personagem,
            emocao = npcDialogoProfessor.dialogoPrincipal.nodulos[0].falas[1].emocao,
            fala = "O feedback dos alunos foi bastante esclarecedor. Eu espero ver você em uma próxima oportunidade, até mais!"
        };
        SistemaDialogo.sistemaDialogo.ComecarDialogo(dialogo.Clone(), null);
    }

    private void EndClassFunction()
    {
        foreach (StudentScript aluno in alunosComentaristas)
        {
            aluno.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }

        if (professor.professor)
        {
            professor.professor.gameObject.GetComponent<NpcDialogo>().enabled = true;
            professor.professor.GetComponent<NpcDialogo>().Restart();
        }
    }

    //Método para que os estudantes possam ser adicionados na lista students
    static public void AddStudent(StudentScript student)
    {
        students.Add(student);

        studentIsProblem.Add(false);
    }

    private void DestruirAgrupamentos()
    {
        GameObject individual = GameObject.Find("CadeirasEMesasIndividual(Clone)");
        if (individual != null)
            Destroy(individual);

        GameObject duplas = GameObject.Find("CadeirasEMesasDuplas(Clone)");
        if (duplas != null)
            Destroy(duplas);

        GameObject grupo = GameObject.Find("CadeirasEMesasGrandeGrupo(Clone)");
        if (grupo != null)
            Destroy(grupo);
    }

    private void CriarAgrupamentos(AgrupamentosEmSala agrupamento)
    {
        switch (agrupamento)
        {
            case AgrupamentosEmSala.Individual:
                Instantiate(AgrupamentoIndividual, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case AgrupamentosEmSala.Duplas:
                Instantiate(AgrupamentoDuplas, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            case AgrupamentosEmSala.GrandeGrupo:
                Instantiate(AgrupamentoGrandeGrupo, new Vector3(0, 0, 0), Quaternion.identity);
                break;
            default:
                break;
        }
    }

    private IEnumerator StartClass()
    {
        timer.text = "Iniciando aula";

        GameManager.UISendoUsada();
        
        momentTimer = classWait;

        while (momentTimer >= 0)
        {
            momentTimer -= Time.deltaTime;

            yield return null;
        }

        momentTimer = momentsTime[classMoment].x;

        previousProblemTime = momentsTime[classMoment].y; //Usa-se o previousProblemTime para o tempo que se tem que esperar para surgir o primeiro problema.

        if (problemQuantity[classMoment] != 0) // impede que seja gerado um tempo para problema caso não haja mais problemas
        {
            nextProblemTime = NextProblem(0f); //Gera o tempo para o primeiro problema. Este pode ter tmin = 0 pois o previousProblemTime controlará para que não se gere problema antes do que se quer.
        }

        var jogador = Player.Instance;
        var historicoMissao = jogador.MissionHistory[jogador.missionID];
        var midia = historicoMissao.chosenMedia[classMoment];
        // Apresentar como objeto da sala a mídia escolhida para o momento
        var midiaNaSalaDeAula = FindObjectOfType<MidiaNaSalaDeAula>();
        if (midiaNaSalaDeAula) midiaNaSalaDeAula.ApresentarMidia(midia);

        // Apresentar no painel dos momentos a mídia escolhida para o momento
        var painelMomentos = FindObjectOfType<PainelMomentosNaAula>();
        if (painelMomentos) painelMomentos.MostrarMidiaSelecionada(classMoment);

        SistemaDialogo.sistemaDialogo.ComecarDialogo(Falas[0], null);

        yield return StartCoroutine(FadeEffect.instance.Fade(1f));
        AgrupamentoFimDeAula.gameObject.SetActive(false);
        students.Clear();
        studentIsProblem.Clear();
        Debug.Log("classMoment = " + classMoment);
        CriarAgrupamentos(Player.Instance.MissionHistory[Player.Instance.missionID].agrupamento[classMoment]);
        yield return StartCoroutine(FadeEffect.instance.Fade(1f));

        while (classMoment < 3)
        {            
            timer.text = "M" + (classMoment + 1) + " - " + Mathf.FloorToInt(momentTimer / 60).ToString("00") + ":" + Mathf.FloorToInt(momentTimer % 60).ToString("00");                    
            

            // Esperar o professor falar com os alunos
            yield return new WaitUntil(() => !SistemaDialogo.sistemaDialogo.transform.GetChild(0).gameObject.activeSelf);
                       
            if (!GameManager.uiSendoUsada) 
            {
                GameManager.UISendoUsada();
            }

            var teacher = TeacherScript.teacher;
            if (teacher)
            {
                var balloon = teacher.GetComponentInChildren<TeacherPopUpBalloon>();
                if (balloon) balloon.ShowBalloon(midia);

                teacher.StartWalk();
            }


            momentTimer -= Time.deltaTime * _timeAcceleration;

            //A primeira parte do cálculo do if se justifica porque o momentTimer é contagem regressiva e o nextProblemTime, não, assim eles são complementares.
            //Como no nextProblemTime já se leva em conta o tempo que tem que sobrar quando surgir o último problema, este pode ser subitraido da duração do momento.
            if (nextProblemTime + momentTimer < momentsTime[classMoment].x - momentsTime[classMoment].z && problemQuantity[classMoment] != 0) //verifica se está na hora do primeiro problema
            {
                GenerateProblem();

                problemQuantity[classMoment] -= 1;

                previousProblemTime = nextProblemTime;

                if (problemQuantity[classMoment] != 0)
                {
                    nextProblemTime = NextProblem(waitBetweenProblems);
                }
            }
            else if (momentTimer < 0)
            {
                classMoment += 1;

                if (classMoment < 3)
                {
                    yield return StartCoroutine(FadeEffect.instance.Fade(1f));
                    DestruirAgrupamentos();
                    students.Clear();
                    studentIsProblem.Clear();
                    Debug.Log("classMoment = " + classMoment);
                    CriarAgrupamentos(Player.Instance.MissionHistory[Player.Instance.missionID].agrupamento[classMoment]);
                    yield return StartCoroutine(FadeEffect.instance.Fade(1f));
                }

                if (classMoment < 3) //a seguir são resetadas as variáveis para se gerar um tempo de problema para o novo momento de aula
                {
                    midia = historicoMissao.chosenMedia[classMoment];
                    if (midiaNaSalaDeAula)
                    {
                        // Esconder a mídia do último momento desta aula
                        midiaNaSalaDeAula.EsconderMidiaAtual();
                        // Apresentar a mídia escolhida pelo jogador para este momento
                        midiaNaSalaDeAula.ApresentarMidia(midia);
                    }
                    // Apresentar no painel dos momentos a mídia escolhida para o momento
                    if (painelMomentos) painelMomentos.MostrarMidiaSelecionada(classMoment);

                    momentTimer = momentsTime[classMoment].x;

                    previousProblemTime = momentsTime[classMoment].y;

                    if (problemQuantity[classMoment] != 0)
                    {
                        nextProblemTime = NextProblem(0f);
                    }

                    SistemaDialogo.sistemaDialogo.ComecarDialogo(Falas[classMoment], null);

                    if (teacher) teacher.PauseWalk();
                }
            }

            CloudsCountdown();
        }

        if (professor.professor)
        {
            professor.professor.FimDaAula();
            yield return new WaitWhile(() => (professor.professor.walkCoroutine != null));
        }

        timer.text = "Aula terminada";

        yield return StartCoroutine(FadeEffect.instance.Fade(1f));

        // Retirar todas as mídias da sala de aula
        if (midiaNaSalaDeAula) midiaNaSalaDeAula.EsconderMidiaAtual();
        DestruirAgrupamentos();
        AgrupamentoFimDeAula.gameObject.SetActive(true);
        // Alterar a sala de acordo com a pontuação do jogador
        Desempenho.instance.TrocarSala();

        yield return StartCoroutine(FadeEffect.instance.Fade(1f));

        GameManager.UINaoSendoUsada();

        if (EndClass != null)
        {
            EndClass();
        }
    }

    private float NextProblem(float tmin)
    {
        float t = 0;

        //momentsTime[classMoment].x é a duração do momento da aula. Deste é subtraido quando tempo tem que restar na aula (momentsTime[classMoment].z)
        // e o tempo de quando surgiu o problema anterior (previousProblemTime) - deve ser um tempo atual ou o tempo que se tem que esperar para surgir o primeiro problema.
        //O resultado disso é quando tempo de ainda se tem para todos os futuros problemas.
        //Isso é divido pelo quantidade de problemas que resta (problemQuantity[classMoment]) para ser ter um tempo máximo para o surgimento do próximo problema.
        t = (momentsTime[classMoment].x - momentsTime[classMoment].z - previousProblemTime) / problemQuantity[classMoment];

        t = Random.Range(tmin, t); //É gerado o número de segundos até o próximo problema.

        //É somado o tempo de quando surgiu o problema anterior (previousProblemTime) - deve ser um tempo atual ou o  tempo que se tem que esperar para surgir o primeiro problema - 
        //para se ter o tempo do próximo problema.
        t += previousProblemTime;

        return t;
    }

    private void CloudsCountdown()
    {
        for (int i = 0; i < clouds.Count; i++) 
        {
            clouds[i].problemDuration -= Time.deltaTime * _timeAcceleration;

            if (clouds[i].problemDuration < 0)
            {
                clouds[i].WrongSolution();

                i -= 1;
            }
        }
    }

    private void GenerateProblem()
    {
        _timeAcceleration = problemTimeAcceleration;

        int n = 0;

        for (int i = 0; i < studentIsProblem.Count; i++) 
        {
            if (!studentIsProblem[i]) 
            {
                n += 1;
            }
        }

        int m = n;

        while (m == n)
        {
            m = Mathf.FloorToInt(Random.Range(0, n));
        }

        n = -1;

        while (m >= 0)
        {
            n += 1;

            if (!studentIsProblem[n])
            {
                m -= 1;
            }
        }

        var worldPos = students[n].transform.position + students[n].cloudOffset;

        var canvasScaler = canvas.GetComponent<CanvasScaler>();
        var xScale = canvasScaler.referenceResolution.x / Screen.width;
        var yScale = canvasScaler.referenceResolution.y / Screen.height;
        var scale = new Vector2(xScale, yScale);

        var cloudPos = mainCamera.WorldToScreenPoint(worldPos) * scale;

        var cloudGameObject = Instantiate(cloud, canvas.transform);

        var cloudRectTransform = cloudGameObject.GetComponent<RectTransform>();
        cloudRectTransform.anchoredPosition = cloudPos;

        cloudGameObject.GetComponent<AudioSource>().Play();

        clouds.Add(cloudGameObject.GetComponent<ProblemCloudScript>());

        clouds[clouds.Count - 1].problemDuration = problemDuration;

        clouds[clouds.Count - 1].studentIndex = n;

        studentIsProblem[n] = true;

    }

    public void RemoveCloud(ProblemCloudScript problem)
    {
        clouds.Remove(problem);

        studentIsProblem [problem.studentIndex ] = false;

        if (clouds.Count == 0)
        {
            _timeAcceleration = timeAcceleration;
        }
    }

    private int TraduzirPontoProblema(int ponto)
    {
        for (int i = 0; i < traducaoPontoProblema.Length; i++)
        {
            if (ponto == traducaoPontoProblema[i].x)
            {
                return traducaoPontoProblema[i].y;
            }
        }

        Debug.Log("Pontuação não tem quantidade de problema");

        return 1;
    }
}