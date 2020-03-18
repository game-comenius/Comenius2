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
    public int missionID { get { return _missionID; } set { _missionID = value; } }

    public Inventory Inventory { get; private set; }

    public ChosenMediaPoints[] MissionHistory = new ChosenMediaPoints[4];

    public string sceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            sceneName = gameObject.scene.name;

            Inventory = new Inventory();

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
}


