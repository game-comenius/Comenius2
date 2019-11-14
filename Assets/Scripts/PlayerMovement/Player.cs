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

    public string sceneName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            sceneName = gameObject.scene.name;

            DontDestroyOnLoad(gameObject);

            Inventory = new Inventory();
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


