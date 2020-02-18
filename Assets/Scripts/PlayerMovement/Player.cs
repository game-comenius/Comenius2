using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ChosenMediaPoints
{
    public ChosenMediaPoints (ItemName[] _chosenMedia, double[] _points, double _totalMissionPoints) {
        chosenMedia = _chosenMedia;
        points = _points;
        totalMissionPoints = _totalMissionPoints;
    }

    public ItemName[] chosenMedia;
    public double[] points;
    public double totalMissionPoints;

}

public class Player : MonoBehaviour
{
    // Objeto Singleton, sempre que quiser a instância dessa classe,
    // use Player.Instance para obter
    public static Player Instance { get; private set; }

    [SerializeField]
    private int _missionID;
    public int missionID { get { return _missionID; } }

    public Inventory Inventory { get; private set; }

    public ChosenMediaPoints[] MissionHistory = new ChosenMediaPoints[4];

    public string sceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            sceneName = gameObject.scene.name;

            DontDestroyOnLoad(gameObject);

            //try
            //{
                Inventory = new Inventory();
            //}
            //catch
            //{
            //    Debug.Log("Chatch");


            //    GameManager i = FindObjectOfType<GameManager>();

            //    Debug.Log(i);

            //    //Inventory = new Inventory();
            //}
            //chosenMedia = new ItemName[3];
            //points = new double[3];
            //totalMissionPoints = 0;
        }
        else
        {
            if (Instance.sceneName == sceneName)
            {
                Instance.transform.position = this.transform.position;

                Instance.GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;

                Instance.sceneName = gameObject.scene.name;
            }

            Destroy(this.gameObject);
        }
    }
}


