using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassManager : MonoBehaviour
{
    static public ClassManager classManager { get; private set; }

    static private List<StudentScript> students = new List<StudentScript>();

    static private List<bool> studentIsProblem = new List<bool>();

    static private List<ProblemCloudScript> clouds = new List<ProblemCloudScript>();

    public delegate void ClassEnded();

    static public event ClassEnded EndClass;

    [SerializeField] private GameObject cloud;

    [SerializeField] private Canvas canvas;

    [Header("Class Moment")]

    [SerializeField] Text timer;

    [Tooltip("Se refere a quanto tempo tem-se que esperar para a aula começar.")]
    [SerializeField] private float classWait;

    [Tooltip ("A componente \"X\" se refere à duração de cada momento da aula, \"Y\" ao  tempo que se tem que esperar para surgir o primeiro problema e \"Z\" ao tempo que tem que sobrar quando surgir o último problema. Tudo em segundos")]
    [SerializeField] private Vector3[] momentsTime = new Vector3[3];

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
        _timeAcceleration = timeAcceleration;

        StartCoroutine(StartClass());
    }

    static public void AddStundent(StudentScript student)
    {
        students.Add(student);

        studentIsProblem.Add(false);
    }

    private IEnumerator StartClass()
    {
        timer.text = "Iniciando aula";
        
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

        while (classMoment < 3)
        {
            timer.text = "M" + (classMoment + 1) + " - " + Mathf.FloorToInt(momentTimer / 60).ToString("00") + ":" + Mathf.FloorToInt(momentTimer % 60).ToString("00");

            yield return null;

            momentTimer -= Time.deltaTime * _timeAcceleration;

            //A primeira parte do cálculo do if se justifica porque o momentTimer e contagem regressiva e o nextProblemTime, não, assim eles são complementares.
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
                    momentTimer = momentsTime[classMoment].x;

                    previousProblemTime = momentsTime[classMoment].y;

                    if (problemQuantity[classMoment] != 0)
                    {
                        nextProblemTime = NextProblem(0f);
                    }
                }
            }

            CloudsCountdown();
        }

        timer.text = "Aula terminada";

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

        Debug.Log(t);

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

        Vector3 pos = Camera.main.WorldToScreenPoint(students[n].transform.position + students[n].cloudOffset) + new Vector3(cloud.GetComponent<RectTransform>().rect.width / 2, cloud.GetComponent<RectTransform>().rect.height / 2, 0);

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
}
