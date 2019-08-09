using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Objeto Singleton, sempre que quiser a instância dessa classe,
    // use Player.Instance para obter
    public static Player Instance { get; private set; }

    public Inventory Inventory { get; private set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Inventory = new Inventory();
        }
        else
        {
            Instance.gameObject.transform.position = this.gameObject.transform.position;
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Inventory.Add(ItemName.Livro);
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            Inventory.Add(ItemName.Cartazes);
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    target.z = transform.position.z;
        //}

        //// movimentação
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}


