using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameComenius.Dialogo;

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
    [SerializeField] private int[] problemQuantity = new int[3];

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

    [Tooltip("Os dois primeiros são os generalistas. O resto, em ordem, correspondem a cada momento de aula.")]
    [SerializeField] private AlunosComentaristas[] alunosComentaristas = new AlunosComentaristas[5];

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
    [SerializeField] private DialogoGeneralista[] falasGeneralistas = new DialogoGeneralista[4];

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

    [System.Serializable] private class ComentarioProfessor : AlunosComentaristas
    {
        [SerializeField] public DialogoGeneralista[] falasGeneralistas = new DialogoGeneralista[4];
    }

    [System.Serializable] private class AlunosComentaristas
    {
        public AgenteAulaScript aluno = null;

        public Vector2Int questIndex = Vector2Int.zero;
    }

    [System.Serializable] private class DialogoGeneralista
    {
        public Vector2 rangeNota = Vector2.zero;

        public Dialogo[] dialogos = new Dialogo[2];
    }
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

        StartCoroutine(StartClass());

        for (int i = 0; i < problemQuantity.Length; i++) 
        {
            problemQuantity[i] = TraduzirPontoProblema((int)Player.Instance.MissionHistory[Player.Instance.missionID].points[i]);
        }

        TeacherSetUp();

        AlunosComentaristasSetUp();

        //Configura o final da aula.
        EndClass += EndClassFunction;
    }

    private void OnDestroy()
    {
        EndClass -= EndClassFunction;
    }

    private void TeacherSetUp()
    {
        professor.aluno.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        NpcDialogo b = professor.aluno.gameObject.GetComponent<NpcDialogo>();

        b.questInfo.isQuest = true;
        b.questInfo.questIndex = professor.questIndex;

        b.dialogoObrigatorio = true;
        b.esperaDialogoObrigatorio = 1f;

        b.interactOffset = new Vector3[] { new Vector3(0.81f, -1.12f, 0) };

        b.questFeita.AddListener(() => { Collider2D collider = GetComponent<Collider2D>(); if (collider != null) { collider.enabled = false; } });

        b.enabled = false;


        for (int j = 0; j < professor.falasGeneralistas.Length; j++)
        {
            if (Player.Instance.MissionHistory[Player.Instance.missionID].totalMissionPoints >= falasGeneralistas[j].rangeNota.x 
                && Player.Instance.MissionHistory[Player.Instance.missionID].totalMissionPoints <= falasGeneralistas[j].rangeNota.y)
            {
                b.dialogoPrincipal = professor.falasGeneralistas[j].dialogos[0].Clone();
            }
        }
    }

    private void AlunosComentaristasSetUp()
    {
        for (int i = 0; i < alunosComentaristas.Length; i++)
        {
            //alunosComentaristas[i].aluno.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
            NpcDialogo d = alunosComentaristas[i].aluno.gameObject.GetComponent<NpcDialogo>();

            d.questInfo.isQuest = true;
            d.questInfo.questIndex = alunosComentaristas[i].questIndex;

            d.dialogoObrigatorio = false;

            d.questFeita.AddListener(() => { Collider2D collider = d.GetComponent<Collider2D>(); if (collider != null) { collider.enabled = false; } });

            if (i <= 1)
            {
                for (int j = 0; j < falasGeneralistas.Length; j++)
                {
                    if (Player.Instance.MissionHistory[Player.Instance.missionID].totalMissionPoints >= falasGeneralistas[j].rangeNota.x 
                        && Player.Instance.MissionHistory[Player.Instance.missionID].totalMissionPoints <= falasGeneralistas[j].rangeNota.y)
                    {
                        d.dialogoPrincipal = falasGeneralistas[j].dialogos[i].Clone();
                    }
                }
            }
            else
            {
                d.dialogoPrincipal = falasSobreMomentos[i - 2].EncontrarFalaCerta(Player.Instance.MissionHistory[Player.Instance.missionID].chosenMedia[i - 2]).Clone();
            }

            //d.enabled = false;
        }
    }

    private void EndClassFunction()
    {
        professor.aluno.gameObject.GetComponent<NpcDialogo>().enabled = true;

        foreach (AlunosComentaristas aluno in alunosComentaristas)
        {
            aluno.aluno.gameObject.GetComponent<NpcDialogo>().enabled = true;
            aluno.aluno.gameObject.GetComponent<PolygonCollider2D>().enabled = true;
        }

        professor.aluno.GetComponent<NpcDialogo>().Restart();
    }

    //Método para que os estudantes possam ser adicionados na lista students
    static public void AddStudent(StudentScript student)
    {
        students.Add(student);

        studentIsProblem.Add(false);
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

        var midiaNaSalaDeAula = FindObjectOfType<MidiaNaSalaDeAula>();
        if (midiaNaSalaDeAula)
        {
            // Apresentar a mídia escolhida pelo jogador para este momento
            var jogador = Player.Instance;
            var historicoMissao = jogador.MissionHistory[jogador.missionID];
            var midia = historicoMissao.chosenMedia[classMoment];
            midiaNaSalaDeAula.ApresentarMidia(midia);
        }

        SistemaDialogo.sistemaDialogo.ComecarDialogo(Falas[0], null);

        while (classMoment < 3)
        {
            timer.text = "M" + (classMoment + 1) + " - " + Mathf.FloorToInt(momentTimer / 60).ToString("00") + ":" + Mathf.FloorToInt(momentTimer % 60).ToString("00");

            // Esperar o professor falar com os alunos
            yield return new WaitUntil(() => !SistemaDialogo.sistemaDialogo.transform.GetChild(0).gameObject.activeSelf);

            if (!GameManager.uiSendoUsada) 
            {
                GameManager.UISendoUsada();
            }

            TeacherScript.teacher.StartWalk();

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

                if (classMoment < 3) //a seguir são resetadas as variáveis para se gerar um tempo de problema para o novo momento de aula
                {
                    if (midiaNaSalaDeAula)
                    {
                        // Esconder a mídia do último momento desta aula
                        midiaNaSalaDeAula.EsconderMidiaAtual();
                        // Apresentar a mídia escolhida pelo jogador para este momento
                        var jogador = Player.Instance;
                        var historicoMissao = jogador.MissionHistory[jogador.missionID];
                        var midia = historicoMissao.chosenMedia[classMoment];
                        midiaNaSalaDeAula.ApresentarMidia(midia);
                    }

                    momentTimer = momentsTime[classMoment].x;

                    previousProblemTime = momentsTime[classMoment].y;

                    if (problemQuantity[classMoment] != 0)
                    {
                        nextProblemTime = NextProblem(0f);
                    }

                    SistemaDialogo.sistemaDialogo.ComecarDialogo(Falas[classMoment], null);

                    TeacherScript.teacher.PauseWalk();
                }
            }

            CloudsCountdown();
        }

        ((TeacherScript)professor.aluno).FimDaAula();

        yield return new WaitWhile(() => ((TeacherScript)professor.aluno).walkCoroutine != null);

        timer.text = "Aula terminada";

        yield return StartCoroutine(FadeEffect.instance.Fade(1f));

        // Retirar todas as mídias da sala de aula
        midiaNaSalaDeAula.EsconderMidiaAtual();

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

        Vector3 pos = mainCamera.WorldToScreenPoint(students[n].transform.position + students[n].cloudOffset);

        GameObject go = Instantiate(cloud, pos, Quaternion.identity, canvas.transform);

        clouds.Add(go.GetComponent<ProblemCloudScript>());

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

        return 3;
    }
}