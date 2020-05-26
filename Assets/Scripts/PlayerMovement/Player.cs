using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ChosenMediaPoints
{
    public ChosenMediaPoints (AgrupamentosEmSala[] _agrupamento, ItemName[] _chosenMedia, int[] _points, int _totalMissionPoints) {
        agrupamento = _agrupamento;
        chosenMedia = _chosenMedia;
        points = _points;
        totalMissionPoints = _totalMissionPoints;
    }

    public AgrupamentosEmSala[] agrupamento;
    public ItemName[] chosenMedia;
    public int[] points;
    public int totalMissionPoints;

}

public class Player : MonoBehaviour
{
    // Objeto Singleton, sempre que quiser a instância dessa classe,
    // use Player.Instance para obter
    public static Player Instance { get; private set; }

    [SerializeField]
    private int _missionID;
    public int missionID { get { return _missionID; } set { _missionID = value; } }

    public Inventory Inventory { get; private set; }

    public ChosenMediaPoints[] MissionHistory = new ChosenMediaPoints[4];

    public string sceneName;

    // variáveis para ajudar a montar o jornal no final do jogo
    public int meioambiente;
    public bool cadeirante;
    public int literatura;
    public bool estranho;
    public int problemasresolvidos;
    public int problemastotais;
    public float porcentagemproblemas;
    public int resultadom1;
    public int resultadom2;
    public int resultadom3;

    private void Awake()
    {
        if (Instance == null)
        {
            sceneName = gameObject.scene.name;

            Inventory = new Inventory();

            meioambiente = 0;
            cadeirante = false;
            literatura = 0;
            estranho = false;
            problemasresolvidos = 0;
            problemastotais = 0;
            porcentagemproblemas = 0f;
            resultadom1 = 0;
            resultadom2 = 0;
            resultadom3 = 0;

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (Instance.sceneName == sceneName)
            {
                Instance.transform.position = this.transform.position;

                Instance.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;

                Instance.sceneName = gameObject.scene.name;

                Instance.missionID = _missionID;
            }

            Destroy(this.gameObject);
        }
    }


    //coleção de métodos para setar as variáveis do jornal
    public void HelpMeioAmbiente() {
        if (meioambiente >= 3)
            meioambiente = 3;
        else
            meioambiente++;
    }

    public void HelpCadeirante()
    {
        cadeirante = true;
    }

    public void HelpLiteratura()
    {
        if (literatura >= 3)
            literatura = 3;
        else
            literatura++;
    }

    public void HelpEstranho()
    {
        estranho = true;
    }

    public void SolvedProblem()
    {
        problemasresolvidos++;
        porcentagemproblemas = (100 * problemasresolvidos) / problemastotais;
    }

    public void SomarProblemasTotais (int newproblems)
    {
        problemastotais += newproblems;
    }

    public void ResultadoM1(int res)
    {
        resultadom1 = res;
    }

    public void ResultadoM2(int res)
    {
        resultadom2 = res;
    }

    public void ResultadoM3(int res)
    {
        resultadom3 = res;
    }

}


