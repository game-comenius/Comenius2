using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Objeto Singleton, sempre que quiser a instância dessa classe,
    // use Player.Instance para obter
    public static Player Instance { get; private set; }

    public Inventory Inventory { get; private set; }

    public ItemName[] chosenMedia;
    public double[] points;
    public double totalMissionPoints;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Inventory = new Inventory();
            //chosenMedia = new ItemName[3];
            //points = new double[3];
            //totalMissionPoints = 0;
        }
        else
        {
            Instance.gameObject.transform.position = this.gameObject.transform.position;
            Destroy(this.gameObject);
        }
    }
}


