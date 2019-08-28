using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassManager : MonoBehaviour
{
    static private ClassManager _classManager;

    static public ClassManager classManager
    {
        get
        {
            return _classManager;
        }
    }

    static private List<StudentScript> students = new List<StudentScript>();

    static private List<bool> studentIsProblem = new List<bool>();

    [SerializeField] private GameObject cloud;

    static private List<ProblemCloudScript> clouds = new List<ProblemCloudScript>();

    [SerializeField] private Canvas canvas;

    [Header("Class Moment")]

    [SerializeField] Text timer;

    [Tooltip ("A componente \"X\" se refere à duração de cada momento da aula, \"Y\" ao  tempo que se tem que esperar para surgir o primeiro problema e \"Z\" ao tempo que tem que sobrar quando surgir o último problema. Tudo em segundos")]
    [SerializeField] private Vector3[] momentsTime = new Vector3[3];

    [Tooltip("Se refere a quantidade de problemas cada momento da aula terá.")]
    [SerializeField] private int[] problemQuantity = new int[3];

    [Tooltip("Aceleração do tempo quando não houverem problemas")]
    [SerializeField] [Range(1, 300)] private float timeAcceleration;

    [Tooltip("Aceleração do tempo quando houverem problemas")]
    [SerializeField] [Range(1, 300)] private float normalTimeAcceleration;

    private float _timeAcceleration;

    [SerializeField] private float problemDuration;

    [SerializeField] private float classWait;

    [Tooltip("Espera entre o início dos problemas")]
    [SerializeField] private float waitBetweenProblems;

    private int classMoment;

    private float momentTimer;

    private float previousProblemTime;

    private float nextProblemTime;

    private void Awake()
    {
        if (_classManager == null)
        {
            _classManager = this;
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

        previousProblemTime = momentsTime[classMoment].y;

        momentTimer = classWait;

        while (momentTimer >= 0)
        {
            momentTimer -= Time.deltaTime;

            yield return null;
        }

        momentTimer = momentsTime[classMoment].x;

        nextProblemTime = NextProblem(0f); //gera o tempo para o primeiro problema

        while (classMoment < 3)
        {
            timer.text = "M" + (classMoment + 1) + " - " + Mathf.FloorToInt(momentTimer / 60).ToString("00") + ":" + Mathf.FloorToInt(momentTimer % 60).ToString("00");

            yield return null;

            momentTimer -= Time.deltaTime * _timeAcceleration;

            if (nextProblemTime + momentTimer < momentsTime[classMoment].x - momentsTime[classMoment].z && problemQuantity[classMoment] != 0) //verifica se está na hora do primeiro problema
            {
                GenerateProblem();

                problemQuantity[classMoment] -= 1;

                previousProblemTime = nextProblemTime;

                if (problemQuantity[classMoment] != 0) // impede que seja gerado um tempo para um problema do próximo momento da aula
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

                    nextProblemTime = momentsTime[classMoment].y;

                    previousProblemTime = nextProblemTime;

                    nextProblemTime = NextProblem(waitBetweenProblems);
                }
            }

            CloudsCountdown();
        }

        timer.text = "Aula terminada";
    }

    private float NextProblem(float tmin)
    {
        float t = 0;

        t = (momentsTime[classMoment].x - momentsTime[classMoment].z - previousProblemTime) / problemQuantity[classMoment];

        t = Random.Range(tmin, t);

        t = t + previousProblemTime;

        //Debug.Log(t);

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
        //Debug.Log("--------------------------------------");

        _timeAcceleration = normalTimeAcceleration;

        int n = 0;

        for (int i = 0; i < studentIsProblem.Count; i++) 
        {
            //Debug.Log(students[i] + " " + studentIsProblem[i]);

            if (!studentIsProblem[i]) 
            {
                n += 1;
            }
        }

        //Debug.Log("a = " + n);

        int m = n;

        while (m == n)
        {
            m = Mathf.FloorToInt(Random.Range(0, n));
        }

        //Debug.Log("m = " + m);

        n = -1;

        while (m >= 0)
        {
            n += 1;

            if (!studentIsProblem[n])
            {
                m -= 1;
            }
        }

        //Debug.Log("nf = " + n);

        Vector3 pos = Camera.main.WorldToScreenPoint(students[n].transform.position + students[n].cloudOffset) + new Vector3(cloud.GetComponent<RectTransform>().rect.width / 2, cloud.GetComponent<RectTransform>().rect.height / 2, 0);

        GameObject go = Instantiate(cloud, pos, Quaternion.identity, canvas.transform);

        clouds.Add(go.GetComponent<ProblemCloudScript>());

        clouds[clouds.Count - 1].problemDuration = problemDuration;

        clouds[clouds.Count - 1].studentIndex = n;

        studentIsProblem[n] = true;

        //gerar problema em students[n]
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
