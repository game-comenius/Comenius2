using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinhaEscolherFeedback : MonoBehaviour {

    private ItemName midia;
    public ItemName Midia
    {
        get { return midia; }
        set
        {
            midia = value;
            GetComponentInChildren<ItemInUserInterface>().ItemName = midia;
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
